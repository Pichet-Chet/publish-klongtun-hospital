using System;
using KTH.MODELS.Custom.Request.MasterThaiDistrict;
using KTH.MODELS.Custom.Request.MasterThaiSubDistrict;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace KTH.REPOSITORIES
{

    public interface IMasterThaiSubDistrictRepository
    {
        public Task<List<MasterThaiSubdistrict>> GetAll(FilterMasterThaiSubDistrictRequest param);

        public Task<MasterThaiSubdistrict> GetById(int id);

        public Task Create(MasterThaiSubdistrict param);

        public Task Update(MasterThaiSubdistrict param);

        public Task<int> CountAllAsync(int districtId);
    }

    public class MasterThaiSubDistrictRepository : IMasterThaiSubDistrictRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public MasterThaiSubDistrictRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<MasterThaiSubdistrict>> GetAll(FilterMasterThaiSubDistrictRequest param)
        {
            List<MasterThaiSubdistrict> output = new List<MasterThaiSubdistrict>();

            try
            {
                var queryable = _context.MasterThaiSubdistricts.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                queryable = queryable.Where(x => x.MasterThaiDistrictsId == param.DistrictId).AsQueryable();

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

                var cacheKey = "MasterThaiSubdistrictsCount";

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

        public async Task<MasterThaiSubdistrict> GetById(int id)
        {
            MasterThaiSubdistrict? Outbound = new MasterThaiSubdistrict();

            try
            {
                var queryable = _context.MasterThaiSubdistricts.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(MasterThaiSubdistrict param)
        {
            try
            {
                _context.MasterThaiSubdistricts.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(MasterThaiSubdistrict param)
        {
            try
            {
                _context.MasterThaiSubdistricts.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> CountAllAsync(int districtId)
        {
            // Use a caching library like MemoryCache, Redis, etc.
            var cacheKey = "MasterThaiSubdistrictsCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.MasterThaiSubdistricts.Where(x => x.MasterThaiDistrictsId == districtId).CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }
    }

}

