using System;
using KTH.MODELS;
using KTH.MODELS.Custom.Request.MasterNationality;
using KTH.MODELS.Custom.Request.TransClientComment;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface ITransClientCommentRepository
    {
        public Task<List<TransClientComment>> GetAll(FilterTransClientCommentRequest param);

        public Task<List<TransClientComment>> GetByClientId(Guid clientId, FilterModel param);

        public Task<TransClientComment> GetById(Guid id);

        public Task Create(TransClientComment param);

        public Task Update(TransClientComment param);

        public Task<bool> DuplicateKey(TransClientComment param, MethodType method);

        public Task<int> CountAllAsync();
    }

    public class TransClientCommentRepository : ITransClientCommentRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public TransClientCommentRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<TransClientComment>> GetAll(FilterTransClientCommentRequest param)
        {
            List<TransClientComment> output = new List<TransClientComment>();

            try
            {
                var queryable = _context.TransClientComments.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();
                    queryable = queryable
                        .Where(x =>
                        (x.Description ?? "").ToLower().Contains(param.TextSearch)
                    ).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.TransClientId))
                {
                    queryable = queryable.Where(x => x.TransClientId != null).AsQueryable();

                    queryable = queryable.Where(x => x.TransClientId == param.TransClientId.ToGuid()).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Description))
                {
                    queryable = queryable.Where(x => x.Description != null).AsQueryable();

                    queryable = queryable.Where(x => x.Description.ToLower() == param.Description.ToLower()).AsQueryable();
                }

                if (param.IsActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();
                }

                #endregion

                output = await queryable.AsNoTracking().ToListAsync();

                var cacheKey = "TransClientCommentsCount";

                _cache.Set(cacheKey, output.Count());

                #region Pagination

                if (param.IsAll == true)
                {
                    output = output.ToList();
                }
                else
                {
                    output = output
                   .Skip((param.PageNumber - 1) * param.PageSize)
                   .Take(param.PageSize)
                   .ToList();
                }

                #endregion
            }
            catch
            {
                throw;
            }

            return output;
        }

        public async Task<List<TransClientComment>> GetByClientId(Guid clientId, FilterModel param)
        {
            List<TransClientComment> output = new List<TransClientComment>();

            try
            {
                var queryable = _context.TransClientComments.AsQueryable();

                #region Filter Data Zone

                queryable = queryable.Where(x => x.TransClientId == clientId).AsQueryable();

                #endregion

                output = await queryable.AsNoTracking().ToListAsync();

                #region Pagination

                if (param.IsAll == true)
                {
                    output = output.ToList();
                }
                else
                {
                    output = output
                   .Skip((param.PageNumber - 1) * param.PageSize)
                   .Take(param.PageSize)
                   .ToList();
                }

                #endregion
            }
            catch
            {
                throw;
            }

            return output;
        }

        public async Task<TransClientComment> GetById(Guid id)
        {
            TransClientComment? Outbound = new TransClientComment();

            try
            {
                var queryable = _context.TransClientComments.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(TransClientComment param)
        {
            try
            {
                _context.TransClientComments.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(TransClientComment param)
        {
            try
            {
                _context.TransClientComments.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(TransClientComment param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.TransClientComments
                    .Where(x =>
                    x.Description.ToLower() == param.Description.ToLower() &&
                    x.TransClientId == param.TransClientId
                    )
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.TransClientComments
                    .Where(x =>
                    x.Description.ToLower() == param.Description.ToLower() &&
                    x.TransClientId == param.TransClientId &&
                    x.Id != param.Id)
                    .AnyAsync();
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<int> CountAllAsync()
        {
            var cacheKey = "TransClientCommentsCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.TransClientComments.CountAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }
    }
}

