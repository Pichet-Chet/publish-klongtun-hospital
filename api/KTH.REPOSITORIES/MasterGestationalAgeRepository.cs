using KTH.MODELS.Custom.Request.MasterGestationalAge;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface IMasterGestationalAgeRepository
    {
        public Task<List<MasterGestationalAge>> GetAll(FilterMasterGestationalAgeRequest param);

        public Task<MasterGestationalAge> GetById(Guid id);

        public Task Create(MasterGestationalAge param);

        public Task Update(MasterGestationalAge param);

        public Task<bool> DuplicateKey(MasterGestationalAge param, MethodType method);

        public Task<int> CountAllAsync();
        public Task<List<MasterGestationalAge>> GetAll();
    }

    public class MasterGestationalAgeRepository : IMasterGestationalAgeRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public MasterGestationalAgeRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<MasterGestationalAge>> GetAll(FilterMasterGestationalAgeRequest param)
        {
            List<MasterGestationalAge> output = new List<MasterGestationalAge>();

            try
            {
                var queryable = _context.MasterGestationalAges.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();

                    queryable = queryable
                        .Where(x =>
                        (x.Name ?? "").ToLower().Contains(param.TextSearch) ||
                        (x.Description ?? "").ToLower().Contains(param.TextSearch)
                    ).AsQueryable();
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

                var cacheKey = "MasterGestationalAgesCount";

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

        public async Task<MasterGestationalAge> GetById(Guid id)
        {
            MasterGestationalAge? Outbound = new MasterGestationalAge();

            try
            {
                var queryable = _context.MasterGestationalAges.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }


        public async Task Create(MasterGestationalAge param)
        {
            try
            {
                _context.MasterGestationalAges.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(MasterGestationalAge param)
        {
            try
            {
                _context.MasterGestationalAges.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(MasterGestationalAge param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.MasterGestationalAges
                    .Where(x =>
                    x.Name.ToLower() == param.Name.ToLower()
                    )
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.MasterGestationalAges
                    .Where(x =>
                    x.Name.ToLower() == param.Name.ToLower() &&
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
            var cacheKey = "MasterGestationalAgesCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.MasterGestationalAges.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }

        public async Task<List<MasterGestationalAge>> GetAll()
        {
            return await _context.MasterGestationalAges.Where(a => a.IsActive).ToListAsync();
        }
    }
}

