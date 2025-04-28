using KTH.MODELS.Custom.Request.MasterReferralFrom;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface IMasterReferralFromRepository
    {
        public Task<List<MasterReferralFrom>> GetAll(FilterMasterReferralFromRequest param);

        public Task<MasterReferralFrom> GetById(Guid id);

        public Task<MasterReferralFrom> GetByName(string name);

        public Task Create(MasterReferralFrom param);

        public Task Update(MasterReferralFrom param);

        public Task<bool> DuplicateKey(MasterReferralFrom param, MethodType method);

        public Task<int> CountAllAsync();
    }

    public class MasterReferralFromRepository : IMasterReferralFromRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public MasterReferralFromRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<MasterReferralFrom>> GetAll(FilterMasterReferralFromRequest param)
        {
            List<MasterReferralFrom> output = new List<MasterReferralFrom>();

            try
            {
                var queryable = _context.MasterReferralFroms.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();
                    queryable = queryable
                        .Where(x =>
                        x.Name.ToLower().Contains(param.TextSearch)
                    ).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Name))
                {
                    queryable = queryable.Where(x => x.Name != null).AsQueryable();

                    queryable = queryable.Where(x => x.Name.ToLower() == param.Name.ToLower()).AsQueryable();
                }

                if (param.IsActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();
                }

                #endregion

                output = await queryable.AsNoTracking().ToListAsync();

                var cacheKey = "MasterReferralFromCount";

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

        public async Task<MasterReferralFrom> GetById(Guid id)
        {
            MasterReferralFrom? Outbound = new MasterReferralFrom();

            try
            {
                var queryable = _context.MasterReferralFroms.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<MasterReferralFrom> GetByName(string name)
        {
            MasterReferralFrom? Outbound = new MasterReferralFrom();

            try
            {
                var queryable = _context.MasterReferralFroms.Where(x => x.Name == name).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(MasterReferralFrom param)
        {
            try
            {
                _context.MasterReferralFroms.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(MasterReferralFrom param)
        {
            try
            {
                _context.MasterReferralFroms.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(MasterReferralFrom param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.MasterReferralFroms
                    .Where(x => x.Name.ToLower() == param.Name.ToLower())
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.MasterReferralFroms
                    .Where(x => x.Name.ToLower() == param.Name.ToLower() &&
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
            var cacheKey = "MasterReferralFromCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.MasterReferralFroms.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }
    }
}

