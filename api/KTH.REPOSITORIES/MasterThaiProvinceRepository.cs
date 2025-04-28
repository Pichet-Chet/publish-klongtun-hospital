using System;
using KTH.MODELS;
using KTH.MODELS.Custom.Request.MasterThaiProvince;
using KTH.MODELS.Custom.Request.SysConfiguration;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace KTH.REPOSITORIES
{
    public interface IMasterThaiProvinceRepository
    {
        public Task<List<MasterThaiProvince>> GetAll(FilterMasterThaiProvinceRequest param);

        public Task<MasterThaiProvince> GetById(int id);

        public Task Create(MasterThaiProvince param);

        public Task Update(MasterThaiProvince param);

        public Task<int> CountAllAsync();
    }

    public class MasterThaiProvinceRepository : IMasterThaiProvinceRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public MasterThaiProvinceRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<MasterThaiProvince>> GetAll(FilterMasterThaiProvinceRequest param)
        {
            List<MasterThaiProvince> output = new List<MasterThaiProvince>();

            try
            {
                var queryable = _context.MasterThaiProvinces.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();
                    queryable = queryable
                        .Where(x =>
                        x.NameTh.ToLower().Contains(param.TextSearch) ||
                        x.NameEn.ToLower().Contains(param.TextSearch) ||
                        (x.Description ?? "").ToLower().Contains(param.TextSearch)
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

                if (param.IsActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();

                }

                #endregion

                output = await queryable.AsNoTracking().ToListAsync();

                var cacheKey = "MasterThaiProvinceCount";

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

        public async Task<MasterThaiProvince> GetById(int id)
        {
            MasterThaiProvince? Outbound = new MasterThaiProvince();

            try
            {
                var queryable = _context.MasterThaiProvinces.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(MasterThaiProvince param)
        {
            try
            {
                _context.MasterThaiProvinces.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(MasterThaiProvince param)
        {
            try
            {
                _context.MasterThaiProvinces.Update(param);

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
            var cacheKey = "MasterThaiProvinceCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.MasterThaiProvinces.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }
    }
}

