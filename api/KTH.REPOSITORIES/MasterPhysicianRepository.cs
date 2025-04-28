using KTH.MODELS.Custom.Request.MasterPhysician;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface IMasterPhysicianRepository
    {
        public Task<List<MasterPhysician>> GetAll(FilterMasterPhysicianRequest param);

        public Task<MasterPhysician> GetById(Guid id);

        public Task<MasterPhysician> GetByName(string name);

        public Task Create(MasterPhysician param);

        public Task Update(MasterPhysician param);

        public Task<bool> DuplicateKey(MasterPhysician param, MethodType method);

        public Task<int> CountAllAsync();
    }

    public class MasterPhysicianRepository : IMasterPhysicianRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public MasterPhysicianRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<MasterPhysician>> GetAll(FilterMasterPhysicianRequest param)
        {
            List<MasterPhysician> output = new List<MasterPhysician>();

            try
            {
                var queryable = _context.MasterPhysicians.AsQueryable();

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

                var cacheKey = "MasterPhysicianCount";

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

        public async Task<MasterPhysician> GetById(Guid id)
        {
            MasterPhysician? Outbound = new MasterPhysician();

            try
            {
                var queryable = _context.MasterPhysicians.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<MasterPhysician> GetByName(string name)
        {
            MasterPhysician? Outbound = new MasterPhysician();

            try
            {
                var queryable = _context.MasterPhysicians.Where(x => x.Name == name).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(MasterPhysician param)
        {
            try
            {
                _context.MasterPhysicians.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(MasterPhysician param)
        {
            try
            {
                _context.MasterPhysicians.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(MasterPhysician param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.MasterPhysicians
                    .Where(x => x.Name.ToLower() == param.Name.ToLower())
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.MasterPhysicians
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
            var cacheKey = "MasterPhysicianCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.MasterPhysicians.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }
    }
}

