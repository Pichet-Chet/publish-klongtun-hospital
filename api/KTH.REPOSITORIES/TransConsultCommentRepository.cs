using System;
using KTH.MODELS;
using KTH.MODELS.Custom.Request.TransConsultComment;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{

    public interface ITransConsultCommentRepository
    {
        public Task<List<TransConsultComment>> GetAll(FilterTransConsultCommentRequest param);

        public Task<List<TransConsultComment>> GetByCaseId(Guid caseId, FilterModel param);

        public Task<TransConsultComment> GetById(Guid id);

        public Task Create(TransConsultComment param);

        public Task Update(TransConsultComment param);

        public Task<bool> DuplicateKey(TransConsultComment param, MethodType method);

        public Task<int> CountAllAsync();
    }

    public class TransConsultCommentRepository : ITransConsultCommentRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public TransConsultCommentRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<TransConsultComment>> GetAll(FilterTransConsultCommentRequest param)
        {
            List<TransConsultComment> output = new List<TransConsultComment>();

            try
            {
                var queryable = _context.TransConsultComments.AsQueryable();

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

                var cacheKey = "TransConsultCommentsCount";

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

        public async Task<List<TransConsultComment>> GetByCaseId(Guid caseId, FilterModel param)
        {
            List<TransConsultComment> output = new List<TransConsultComment>();

            try
            {
                var queryable = _context.TransConsultComments.AsQueryable();

                #region Filter Data Zone

                queryable = queryable.Where(x => x.TransCaseId == caseId).AsQueryable();

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

        public async Task<TransConsultComment> GetById(Guid id)
        {
            TransConsultComment? Outbound = new TransConsultComment();

            try
            {
                var queryable = _context.TransConsultComments.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(TransConsultComment param)
        {
            try
            {
                _context.TransConsultComments.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(TransConsultComment param)
        {
            try
            {
                _context.TransConsultComments.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(TransConsultComment param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.TransConsultComments
                    .Where(x =>
                    x.Description.ToLower() == param.Description.ToLower() &&
                    x.TransCaseId == param.TransCaseId
                    )
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.TransConsultComments
                    .Where(x =>
                    x.Description.ToLower() == param.Description.ToLower() &&
                    x.TransCaseId == param.TransCaseId &&
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
            var cacheKey = "TransConsultCommentsCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.TransConsultComments.CountAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }
    }

}

