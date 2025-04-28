using KTH.MODELS.Custom.Request.MasterSaleGroup;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface IMasterSaleGroupRepository
    {
        public Task<List<MasterSaleGroup>> GetAll(FilterMasterSaleGroupRequest param);

        public Task<MasterSaleGroup> GetById(Guid id);

        public Task<MasterSaleGroup> GetByCode(string code);

        public Task<MasterSaleGroup> GetByName(string name);

        public Task Create(MasterSaleGroup param);

        public Task Update(MasterSaleGroup param);

        public Task<bool> DuplicateKey(MasterSaleGroup param, MethodType method);

        public Task<int> CountAllAsync();

        Task<List<MasterSaleGroup>> GetAll();
    }


    public class MasterSaleGroupRepository : IMasterSaleGroupRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public MasterSaleGroupRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<MasterSaleGroup>> GetAll(FilterMasterSaleGroupRequest param)
        {
            List<MasterSaleGroup> output = new List<MasterSaleGroup>();

            try
            {
                var queryable = _context.MasterSaleGroups.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();
                    queryable = queryable
                        .Where(x =>
                        x.Code.ToLower().Contains(param.TextSearch) ||
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

                var cacheKey = "MasterSaleGroupsCount";

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

        public async Task<MasterSaleGroup> GetById(Guid id)
        {
            MasterSaleGroup? Outbound = new MasterSaleGroup();

            try
            {
                var queryable = _context.MasterSaleGroups.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<MasterSaleGroup> GetByCode(string code)
        {
            MasterSaleGroup? Outbound = new MasterSaleGroup();

            try
            {
                var queryable = _context.MasterSaleGroups.Where(x => x.Code == code).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<MasterSaleGroup> GetByName(string name)
        {
            MasterSaleGroup? Outbound = new MasterSaleGroup();

            try
            {
                var queryable = _context.MasterSaleGroups.Where(x => x.Name == name).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(MasterSaleGroup param)
        {
            try
            {
                _context.MasterSaleGroups.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(MasterSaleGroup param)
        {
            try
            {
                _context.MasterSaleGroups.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(MasterSaleGroup param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.MasterSaleGroups
                    .Where(x => x.Name.ToLower() == param.Name.ToLower())
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.MasterSaleGroups
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
            var cacheKey = "MasterSaleGroupsCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.MasterSaleGroups.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }

        public async Task<List<MasterSaleGroup>> GetAll()
        {
            return await _context.MasterSaleGroups.Where(a => a.IsActive).ToListAsync();
        }
    }
}

