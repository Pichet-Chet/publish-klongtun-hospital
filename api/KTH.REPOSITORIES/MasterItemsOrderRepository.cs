using KTH.MODELS.Custom.Request.MasterGestationalAge;
using KTH.MODELS.Custom.Request.MasterItemsOrder;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface IMasterItemsOrderRepository
    {
        public Task<List<MasterItemsOrder>> GetAll(FilterMasterItemsOrderRequest param);

        public Task<MasterItemsOrder> GetById(Guid id);

        public Task Create(MasterItemsOrder param);

        public Task Update(MasterItemsOrder param);

        public Task<bool> DuplicateKey(MasterItemsOrder param, MethodType method);

        public Task<int> CountAllAsync();

        public Task<bool> VerifyId(Guid id);

        Task<List<MasterItemsOrder>> GetAll();

        Task<List<MasterItemsOrder>> GetWithGroupId(Guid id);
        Task<List<MasterItemsOrder>> GetWithGroupId(List<Guid> listMasterItemGroupId);

    }

    public class MasterItemsOrderRepository : IMasterItemsOrderRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public MasterItemsOrderRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<MasterItemsOrder>> GetAll(FilterMasterItemsOrderRequest param)
        {
            List<MasterItemsOrder> output = new List<MasterItemsOrder>();

            try
            {
                var queryable = _context.MasterItemsOrders.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();
                    queryable = queryable
                        .Where(x =>
                        (x.Name ?? "").ToLower().Contains(param.TextSearch) ||
                        (x.Description ?? "").ToLower().Contains(param.TextSearch)
                    ).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.MasterItemsOrderGroupId))
                {
                    queryable = queryable.Where(x => x.MasterItemsOrderGroupId != null).AsQueryable();

                    queryable = queryable.Where(x => x.MasterItemsOrderGroupId == param.MasterItemsOrderGroupId.ToGuid()).AsQueryable();
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

                output = await queryable.AsNoTracking().Include(x => x.MasterItemsOrderGroup).ToListAsync();

                var cacheKey = "MasterItemsOrdersCount";

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

        public async Task<MasterItemsOrder> GetById(Guid id)
        {
            MasterItemsOrder? Outbound = new MasterItemsOrder();

            try
            {
                var queryable = _context.MasterItemsOrders.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().Include(x => x.MasterItemsOrderGroup).FirstOrDefaultAsync();
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
                result = await _context.MasterItemsOrders.Where(x => x.Id == id).AnyAsync();
            }
            catch
            {
                throw;
            }

            return result;
        }


        public async Task Create(MasterItemsOrder param)
        {
            try
            {
                _context.MasterItemsOrders.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(MasterItemsOrder param)
        {
            try
            {
                _context.MasterItemsOrders.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(MasterItemsOrder param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.MasterItemsOrders
                    .Where(x =>
                    x.Name.ToLower() == param.Name.ToLower()
                    )
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.MasterItemsOrders
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
            var cacheKey = "MasterItemsOrdersCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.MasterItemsOrders.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }

        public async Task<List<MasterItemsOrder>> GetAll()
        {
            return await _context.MasterItemsOrders.Include(x => x.MasterItemsOrderGroup).Where(a => a.IsActive).ToListAsync();
        }

        public async Task<List<MasterItemsOrder>> GetWithGroupId(Guid id)
        {
            return await _context.MasterItemsOrders.Where(a => a.MasterItemsOrderGroupId == id).ToListAsync();
        }

        public async Task<List<MasterItemsOrder>> GetWithGroupId(List<Guid> listMasterItemGroupId)
        {
            return await _context.MasterItemsOrders.Where(a => a.MasterItemsOrderGroupId.HasValue && listMasterItemGroupId.Contains(a.MasterItemsOrderGroupId.Value)).ToListAsync();
        }
    }
}

