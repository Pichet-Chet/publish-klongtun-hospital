using System;
using KTH.MODELS.Custom.Request.MasterThaiDistrict;
using KTH.MODELS.Custom.Request.MasterThaiProvince;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace KTH.REPOSITORIES
{
    public interface IMasterThaiDistrictRepository
    {
        public Task<List<MasterThaiDistrict>> GetAll(FilterMasterThaiDistrictRequest param);

        public Task<MasterThaiDistrict> GetById(int id);

        public Task Create(MasterThaiDistrict param);

        public Task Update(MasterThaiDistrict param);

        public Task<int> CountAllAsync(int provinceId);
    }

    public class MasterThaiDistrictRepository : IMasterThaiDistrictRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public MasterThaiDistrictRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<MasterThaiDistrict>> GetAll(FilterMasterThaiDistrictRequest param)
        {
            List<MasterThaiDistrict> output = new List<MasterThaiDistrict>();

            try
            {
                var queryable = _context.MasterThaiDistricts.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                queryable = queryable.Where(x => x.MasterThaiProvincesId == param.ProvinceId).AsQueryable();

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

                var cacheKey = "MasterThaiDistrictCount";

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

        public async Task<MasterThaiDistrict> GetById(int id)
        {
            MasterThaiDistrict? Outbound = new MasterThaiDistrict();

            try
            {
                var queryable = _context.MasterThaiDistricts.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(MasterThaiDistrict param)
        {
            try
            {
                _context.MasterThaiDistricts.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(MasterThaiDistrict param)
        {
            try
            {
                _context.MasterThaiDistricts.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> CountAllAsync(int provinceId)
        {
            // Use a caching library like MemoryCache, Redis, etc.
            var cacheKey = "MasterThaiDistrictCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.MasterThaiDistricts.Where(x => x.MasterThaiProvincesId == provinceId).CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }
    }
}

