using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using KTH.MODELS.Custom.Request;
using static KTH.MODELS.Constants.ConstantsPermission;
using KTH.MODELS.Custom.Request.MasterCountry;
using Microsoft.Extensions.Caching.Memory;

namespace KTH.REPOSITORIES
{
    public interface IMasterCountryRepository
    {
        public Task<List<MasterCountry>> GetAll(FilterMasterCountryRequest param);

        public Task<MasterCountry> GetById(Guid id);

        public Task<bool> VerifyPhoneCode(string code);

        public Task Create(MasterCountry param);

        public Task Update(MasterCountry param);

        public Task<bool> DuplicateKey(MasterCountry param, MethodType method);

        public Task<int> CountAllAsync();
    }

    public class MasterCountryRepository : IMasterCountryRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public MasterCountryRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<MasterCountry>> GetAll(FilterMasterCountryRequest param)
        {
            List<MasterCountry> output = new List<MasterCountry>();

            try
            {
                var queryable = _context.MasterCountries.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();

                    queryable = queryable
                        .Where(x =>
                        x.NameTh.ToLower().Contains(param.TextSearch) ||
                        x.NameEn.ToLower().Contains(param.TextSearch) ||
                        x.Code.ToLower().Contains(param.TextSearch)
                    ).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.NameEn))
                {
                    queryable = queryable.Where(x => x.NameEn != null).AsQueryable();

                    queryable = queryable.Where(x => x.NameEn.ToLower() == param.NameEn.ToLower()).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.NameTh))
                {
                    queryable = queryable.Where(x => x.NameTh != null).AsQueryable();

                    queryable = queryable.Where(x => x.NameTh.ToLower() == param.NameTh.ToLower()).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Code))
                {
                    queryable = queryable.Where(x => x.Code != null).AsQueryable();

                    queryable = queryable.Where(x => x.Code.ToLower() == param.Code.ToLower()).AsQueryable();
                }

                if (param.IsActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();

                }

                #endregion

                output = await queryable.AsNoTracking().ToListAsync();

                var cacheKey = "MasterCountryCount";

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

        public async Task<MasterCountry> GetById(Guid id)
        {
            MasterCountry? Outbound = new MasterCountry();

            try
            {
                var queryable = _context.MasterCountries.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(MasterCountry param)
        {
            try
            {
                _context.MasterCountries.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(MasterCountry param)
        {
            try
            {
                _context.MasterCountries.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> VerifyPhoneCode(string code)
        {
            bool result = false;

            try
            {
                result = await _context.MasterCountries
                    .Where(x => x.TelephoneCode.ToLower() == code.ToLower())
                    .AnyAsync();
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<bool> DuplicateKey(MasterCountry param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.MasterCountries
                    .Where(x => x.Code.ToLower() == param.Code.ToLower())
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.MasterCountries
                    .Where(x => x.Code.ToLower() == param.Code.ToLower() &&
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
            var cacheKey = "MasterCountryCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.MasterCountries.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }
    }
}

