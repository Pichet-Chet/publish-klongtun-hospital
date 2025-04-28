using KTH.MODELS.Custom.Request.MasterGestationalAge;
using KTH.MODELS.Custom.Request.MasterItemsOrderGroup;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface IMasterItemsOrderGroupRepository
    {
        public Task<List<MasterItemsOrderGroup>> GetAll(FilterMasterItemsOrderGroupRequest param);

        public Task<MasterItemsOrderGroup> GetById(Guid id);

        public Task Create(MasterItemsOrderGroup param);

        public Task Update(MasterItemsOrderGroup param);

        public Task<bool> DuplicateKey(MasterItemsOrderGroup param, MethodType method);

        public Task<int> CountAllAsync();

        public Task<bool> VerifyId(Guid id);

        Task<List<MasterItemsOrderGroup>> GetAll();

        Task<MasterItemsOrderGroup> GetByCode(string code);
        Task<List<MasterItemsOrderGroup>> GetByCode(List<string> list);
    }

    public class MasterItemsOrderGroupRepository : IMasterItemsOrderGroupRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public MasterItemsOrderGroupRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<MasterItemsOrderGroup>> GetAll(FilterMasterItemsOrderGroupRequest param)
        {
            List<MasterItemsOrderGroup> output = new List<MasterItemsOrderGroup>();

            try
            {
                var queryable = _context.MasterItemsOrderGroups.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();
                    queryable = queryable
                        .Where(x =>
                        x.Name.ToLower().Contains(param.TextSearch) ||
                        (x.Description ?? "").ToLower().Contains(param.TextSearch)
                    ).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Name))
                {
                    queryable = queryable.Where(x => x.Name != null).AsQueryable();

                    queryable = queryable.Where(x => x.Name.ToLower() == param.Name.ToLower()).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Description))
                {
                    queryable = queryable.Where(x => x.Description != null).AsQueryable();

                    queryable = queryable.Where(x => x.Description.ToLower() == param.Description.ToLower()).AsQueryable();
                }

                if (param.IsActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();
                }

                #endregion

                output = await queryable.AsNoTracking().ToListAsync();

                var cacheKey = "MasterItemsOrderGroupsCount";

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

        public async Task<MasterItemsOrderGroup> GetById(Guid id)
        {
            MasterItemsOrderGroup? Outbound = new MasterItemsOrderGroup();

            try
            {
                var queryable = _context.MasterItemsOrderGroups.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<bool> VerifyId(Guid id)
        {
            bool result = false;

            try
            {
                result = await _context.MasterItemsOrderGroups.Where(x => x.Id == id).AnyAsync();
            }
            catch
            {
                throw;
            }

            return result;
        }


        public async Task Create(MasterItemsOrderGroup param)
        {
            try
            {
                _context.MasterItemsOrderGroups.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(MasterItemsOrderGroup param)
        {
            try
            {
                _context.MasterItemsOrderGroups.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(MasterItemsOrderGroup param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.MasterItemsOrderGroups
                    .Where(x =>
                    x.Name.ToLower() == param.Name.ToLower()
                    )
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.MasterItemsOrderGroups
                    .Where(x =>
                    x.Name.ToLower() == param.Name.ToLower() &&
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
            var cacheKey = "MasterItemsOrderGroupsCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.MasterItemsOrderGroups.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }

        public async Task<List<MasterItemsOrderGroup>> GetAll()
        {
            return await _context.MasterItemsOrderGroups.Where(a => a.IsActive).ToListAsync();
        }

        public async Task<MasterItemsOrderGroup> GetByCode(string code)
        {
            return await _context.MasterItemsOrderGroups.FirstOrDefaultAsync(a=> a.Code == code);
        }

        public async Task<List<MasterItemsOrderGroup>> GetByCode(List<string> list)
        {
            return await _context.MasterItemsOrderGroups.Where(a => list.Contains(a.Code)).Include(a=> a.MasterItemsOrders).ToListAsync();
        }
    }
}

