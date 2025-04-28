using System;
using KTH.MODELS;
using KTH.MODELS.Custom.Request.MasterCountry;
using KTH.MODELS.Custom.Request.SysConfiguration;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface ISysConfigurationRepository
    {
        public Task<List<SysConfiguration>> GetAll(FilterSysConfigurationRequest param);

        public Task<List<SysConfiguration>> GetByGroup(string group, FilterModel param);

        public Task<SysConfiguration> GetById(Guid id);

        public Task<SysConfiguration> GetByKey(string key);

        public Task Create(SysConfiguration param);

        public Task Update(SysConfiguration param);

        public Task<bool> DuplicateKey(SysConfiguration param, MethodType method);

        public Task<int> CountAllAsync();
        Task<List<SysConfiguration>> GetAll();
    }

    public class SysConfigurationRepository : ISysConfigurationRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public SysConfigurationRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<SysConfiguration>> GetAll(FilterSysConfigurationRequest param)
        {
            List<SysConfiguration> output = new List<SysConfiguration>();

            try
            {
                var queryable = _context.SysConfigurations.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();
                    queryable = queryable
                        .Where(x =>
                        x.Key.ToLower().Contains(param.TextSearch) ||
                        x.Value.ToLower().Contains(param.TextSearch) ||
                        (x.Group ?? "").ToLower().Contains(param.TextSearch) ||
                        (x.Description ?? "").ToLower().Contains(param.TextSearch)
                    ).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Key))
                {
                    queryable = queryable.Where(x => x.Key != null).AsQueryable();

                    queryable = queryable.Where(x => x.Key.ToLower() == param.Key.ToLower()).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Value))
                {
                    queryable = queryable.Where(x => x.Value != null).AsQueryable();

                    queryable = queryable.Where(x => x.Value.ToLower() == param.Value.ToLower()).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Group))
                {
                    queryable = queryable.Where(x => x.Group != null).AsQueryable();

                    queryable = queryable.Where(x => x.Group.ToLower() == param.Group.ToLower()).AsQueryable();
                }

                if (param.IsActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();

                }

                #endregion

                output = await queryable.AsNoTracking().ToListAsync();

                var cacheKey = "SysConfigurationCount";

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

        public async Task<List<SysConfiguration>> GetByGroup(string group, FilterModel param)
        {
            List<SysConfiguration> output = new List<SysConfiguration>();

            try
            {
                var queryable = _context.SysConfigurations.AsQueryable();

                #region Filter Data Zone

                queryable = queryable.Where(x => x.Group.ToLower() == group.ToLower()).AsQueryable();

                #endregion

                output = await queryable.AsNoTracking().ToListAsync();

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

        public async Task<SysConfiguration> GetById(Guid id)
        {
            SysConfiguration? Outbound = new SysConfiguration();

            try
            {
                var queryable = _context.SysConfigurations.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<SysConfiguration> GetByKey(string key)
        {
            SysConfiguration? Outbound = new SysConfiguration();

            try
            {
                var queryable = _context.SysConfigurations.Where(x => x.Key == key).AsQueryable();

                Outbound = await queryable.FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(SysConfiguration param)
        {
            try
            {
                _context.SysConfigurations.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(SysConfiguration param)
        {
            try
            {
                _context.SysConfigurations.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(SysConfiguration param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.SysConfigurations
                    .Where(x => x.Key.ToLower() == param.Key.ToLower())
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.SysConfigurations
                    .Where(x => x.Key.ToLower() == param.Key.ToLower() &&
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
            var cacheKey = "SysConfigurationCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.SysConfigurations.CountAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }

        public async Task<List<SysConfiguration>> GetAll()
        {
            return await _context.SysConfigurations.Where(a => a.IsActive).ToListAsync();
        }
    }
}

