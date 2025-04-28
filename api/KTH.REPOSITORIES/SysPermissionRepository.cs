using KTH.MODELS.Custom.Request;
using KTH.MODELS.Custom.Request.SysPermission;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface ISysPermissionRepository
    {
        public Task<List<SysPermission>> GetAll(FilterSysPermissionRequest param);

        public Task<List<SysPermission>> GetByRole(Guid roleId);

        public Task<SysPermission> GetById(Guid id);

        public Task Create(SysPermission param);

        public Task Update(SysPermission param);

        public Task<int> CountAllAsync();
    }

    public class SysPermissionRepository : ISysPermissionRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public SysPermissionRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<SysPermission>> GetAll(FilterSysPermissionRequest param)
        {
            List<SysPermission> output = new List<SysPermission>();

            try
            {
                var queryable = _context.SysPermissions.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();
                    queryable = queryable
                        .Where(x =>
                        x.Page.ToLower().Contains(param.TextSearch) ||
                        (x.Action ?? "").ToLower().Contains(param.TextSearch)
                    ).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Page))
                {
                    queryable = queryable.Where(x => x.Page != null).AsQueryable();

                    queryable = queryable.Where(x => x.Page.ToLower() == param.Page.ToLower()).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Action))
                {
                    queryable = queryable.Where(x => x.Action != null).AsQueryable();

                    queryable = queryable.Where(x => x.Action.ToLower() == param.Action.ToLower()).AsQueryable();
                }

                if (param.IsActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();
                }

                #endregion

                output = await queryable.AsNoTracking().ToListAsync();

                var cacheKey = "SysPermissionCount";

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

        public async Task<List<SysPermission>> GetByRole(Guid roleId)
        {
            List<SysPermission> Outbound = new List<SysPermission>();

            try
            {
                var queryable = _context.SysPermissions.Where(x => x.SysRoleId == roleId).AsQueryable();

                Outbound = await queryable.AsNoTracking().ToListAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<SysPermission> GetById(Guid id)
        {
            SysPermission? Outbound = new SysPermission();

            try
            {
                var queryable = _context.SysPermissions.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(SysPermission param)
        {
            try
            {
                _context.SysPermissions.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(SysPermission param)
        {
            try
            {
                _context.SysPermissions.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> CountAllAsync()
        {
            var cacheKey = "SysPermissionCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.SysPermissions.CountAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }
    }
}

