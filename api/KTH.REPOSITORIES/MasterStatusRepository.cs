using KTH.MODELS.Custom.Request.MasterStatus;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface IMasterStatusRepository
    {
        public Task<List<MasterStatus>> GetAll(FilterMasterStatusRequest param);

        public Task<MasterStatus> GetById(Guid id);

        public Task<MasterStatus> GetByStatusCode(string code);

        public Task Create(MasterStatus param);

        public Task Update(MasterStatus param);

        public Task<bool> VerifyStatusCode(string code);

        public Task<bool> DuplicateKey(MasterStatus param, MethodType method);

        public Task<int> CountAllAsync();
    }

    public class MasterStatusRepository : IMasterStatusRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public MasterStatusRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<MasterStatus>> GetAll(FilterMasterStatusRequest param)
        {
            List<MasterStatus> output = new List<MasterStatus>();

            try
            {
                var queryable = _context.MasterStatuses.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();
                    queryable = queryable
                        .Where(x =>
                        x.Name.ToLower().Contains(param.TextSearch) ||
                        (x.Description ?? "").ToLower().Contains(param.TextSearch)
                    ).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Group))
                {
                    queryable = queryable.Where(x => x.Group != null).AsQueryable();

                    queryable = queryable.Where(x => x.Group.ToLower() == param.Group.ToLower()).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Name))
                {
                    queryable = queryable.Where(x => x.Name != null).AsQueryable();

                    queryable = queryable.Where(x => x.Name.ToLower() == param.Name.ToLower()).AsQueryable();
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

                var cacheKey = "MasterStatusesCount";

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

        public async Task<MasterStatus> GetById(Guid id)
        {
            MasterStatus? Outbound = new MasterStatus();

            try
            {
                var queryable = _context.MasterStatuses.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<MasterStatus> GetByStatusCode(string code)
        {
            MasterStatus? Outbound = new MasterStatus();

            try
            {
                var queryable = _context.MasterStatuses.Where(x => x.Code == code).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<bool> VerifyStatusCode(string code)
        {
            bool result = false;

            try
            {
                result = await _context.MasterStatuses.Where(x => x.Code == code).AnyAsync();
            }
            catch
            {
                throw;
            }

            return result;
        }


        public async Task Create(MasterStatus param)
        {
            try
            {
                _context.MasterStatuses.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(MasterStatus param)
        {
            try
            {
                _context.MasterStatuses.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(MasterStatus param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.MasterStatuses
                    .Where(x =>
                    x.Group.ToLower() == param.Group.ToLower() &&
                    x.Code == param.Code
                    )
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.MasterStatuses
                    .Where(x =>
                    x.Group.ToLower() == param.Group.ToLower() &&
                    x.Code == param.Code &&
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
            // Use a caching library like MemoryCache, Redis, etc.
            var cacheKey = "MasterStatusesCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.MasterStatuses.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }
    }
}

