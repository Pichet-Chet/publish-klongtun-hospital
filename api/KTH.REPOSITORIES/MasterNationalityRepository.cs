using KTH.MODELS.Custom.Request.MasterNationality;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface IMasterNationalityRepository
    {
        public Task<List<MasterNationality>> GetAll(FilterMasterNationalityRequest param);

        public Task<MasterNationality> GetById(Guid id);

        public Task Create(MasterNationality param);

        public Task Update(MasterNationality param);

        public Task<bool> DuplicateKey(MasterNationality param, MethodType method);

        public Task<int> CountAllAsync();
    }

    public class MasterNationalityRepository : IMasterNationalityRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public MasterNationalityRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<MasterNationality>> GetAll(FilterMasterNationalityRequest param)
        {
            List<MasterNationality> output = new List<MasterNationality>();

            try
            {
                var queryable = _context.MasterNationalities.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();
                    queryable = queryable
                        .Where(x =>
                        x.NameTh.ToLower().Contains(param.TextSearch) ||
                        (x.NameEn ?? "").ToLower().Contains(param.TextSearch)
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

                if (param.IsActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();
                }

                #endregion

                output = await queryable.AsNoTracking().ToListAsync();

                var cacheKey = "MasterNationalityCount";

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

        public async Task<MasterNationality> GetById(Guid id)
        {
            MasterNationality? Outbound = new MasterNationality();

            try
            {
                var queryable = _context.MasterNationalities.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }


        public async Task Create(MasterNationality param)
        {
            try
            {
                _context.MasterNationalities.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(MasterNationality param)
        {
            try
            {
                _context.MasterNationalities.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(MasterNationality param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.MasterNationalities
                    .Where(x =>
                    x.NameTh.ToLower() == param.NameTh.ToLower() &&
                    x.NameEn.ToLower() == param.NameEn.ToLower()
                    )
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.MasterNationalities
                    .Where(x =>
                    x.NameTh.ToLower() == param.NameTh.ToLower() &&
                    x.NameEn.ToLower() == param.NameEn.ToLower() &&
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
            var cacheKey = "MasterNationalityCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.MasterNationalities.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }
    }
}

