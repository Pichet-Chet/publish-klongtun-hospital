using KTH.MODELS.Custom.Request;
using KTH.MODELS.Custom.Request.SysRole;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface ISysRoleRepository
    {
        public Task<List<SysRole>> GetAll(FilterSysRoleRequest param);

        public Task<SysRole> GetById(Guid id);

        public Task<SysRole> GetByName(string name);

        public Task Create(SysRole param);

        public Task Update(SysRole param);

        public Task<bool> DuplicateKey(SysRole param, MethodType method);

        public Task<int> CountAllAsync();

        public Task<List<SysRole>> GetByCode(List<string> listCode);
        public Task<SysRole> GetByCode(string code);
    }

    public class SysRoleRepository : ISysRoleRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public SysRoleRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<SysRole>> GetAll(FilterSysRoleRequest param)
        {
            List<SysRole> output = new List<SysRole>();

            try
            {
                var queryable = _context.SysRoles.AsQueryable();

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

                var cacheKey = "SysRolesCount";

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

        public async Task<SysRole> GetById(Guid id)
        {
            SysRole? Outbound = new SysRole();

            try
            {
                var queryable = _context.SysRoles.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<SysRole> GetByName(string name)
        {
            SysRole? Outbound = new SysRole();

            try
            {
                var queryable = _context.SysRoles.Where(x => x.Name == name).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(SysRole param)
        {
            try
            {
                _context.SysRoles.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(SysRole param)
        {
            try
            {
                _context.SysRoles.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(SysRole param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.SysRoles
                    .Where(x => x.Name.ToLower() == param.Name.ToLower())
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.SysRoles
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
            var cacheKey = "SysRolesCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.SysRoles.CountAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }

        public async Task<List<SysRole>> GetByCode(List<string> listCode)
        {
            try
            {
                return await _context.SysRoles.Where(a => listCode.Contains(a.Code)).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<SysRole> GetByCode(string code)
        {
            try
            {
                return await _context.SysRoles.FirstOrDefaultAsync(a => a.Code == code);
            }
            catch
            {
                throw;
            }
        }
    }
}

