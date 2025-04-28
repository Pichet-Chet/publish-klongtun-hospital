using KTH.MODELS;
using KTH.MODELS.Custom.Request.MasterHospital;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace KTH.REPOSITORIES
{
    public interface IMasterHospitalRepository
    {
        public Task<List<MasterHospital>> GetAll(FilterMasterHospitalRequest param);

        public Task<List<MasterHospital>> GetByProvinceId(int provinceId, FilterModel param);

        public Task<MasterHospital> GetById(Guid id);

        public Task Create(MasterHospital param);

        public Task Update(MasterHospital param);

        public Task<int> CountAllAsync();
    }

    public class MasterHospitalRepository : IMasterHospitalRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public MasterHospitalRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<MasterHospital>> GetAll(FilterMasterHospitalRequest param)
        {
            List<MasterHospital> output = new List<MasterHospital>();

            try
            {
                var queryable = _context.MasterHospitals.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();

                    queryable = queryable
                        .Where(x =>
                        x.NameTh.ToLower().Contains(param.TextSearch) ||
                        (x.NameEn ?? "").ToLower().Contains(param.TextSearch) ||
                        (x.MasterThaiProvincesNameTh ?? "").ToLower().Contains(param.TextSearch)
                    ).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.NameTh))
                {
                    queryable = queryable.Where(x => x.NameTh != null).AsQueryable();

                    queryable = queryable.Where(x => x.NameTh.ToLower() == param.NameTh.ToLower()).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.NameEn))
                {
                    queryable = queryable.Where(x => x.NameEn != null).AsQueryable();

                    queryable = queryable.Where(x => x.NameEn.ToLower() == param.NameEn.ToLower()).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Department))
                {
                    queryable = queryable.Where(x => x.Department != null).AsQueryable();

                    queryable = queryable.Where(x => x.Department.ToLower() == param.Department.ToLower()).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Type))
                {
                    queryable = queryable.Where(x => x.Type != null).AsQueryable();

                    queryable = queryable.Where(x => x.Type.ToLower() == param.Type.ToLower()).AsQueryable();
                }

                if (param.Code != null)
                {
                    queryable = queryable.Where(x => x.Code == param.Code).AsQueryable();

                }

                if (param.IsActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();

                }

                #endregion

                output = await queryable.AsNoTracking().ToListAsync();

                var cacheKey = "MasterHospitalsCount";

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

        public async Task<List<MasterHospital>> GetByProvinceId(int provinceId, FilterModel param)
        {
            List<MasterHospital> output = new List<MasterHospital>();

            try
            {
                var queryable = _context.MasterHospitals.AsQueryable();

                #region Filter Data Zone


                if (provinceId != null)
                {
                    queryable = queryable.Where(x => x.MasterThaiProvincesNameId == provinceId).AsQueryable();
                }

                #endregion

                output = await queryable.AsNoTracking().ToListAsync();

                var cacheKey = "MasterHospitalsCount";

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

        public async Task<MasterHospital> GetById(Guid id)
        {
            MasterHospital? Outbound = new MasterHospital();

            try
            {
                var queryable = _context.MasterHospitals.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(MasterHospital param)
        {
            try
            {
                _context.MasterHospitals.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(MasterHospital param)
        {
            try
            {
                _context.MasterHospitals.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> CountAllAsync()
        {
            // Use a caching library like MemoryCache, Redis, etc.
            var cacheKey = "MasterHospitalsCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.MasterHospitals.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }

    }
}

