using KTH.MODELS;
using KTH.MODELS.Custom.Request.TransSale;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq;
using System.Linq.Dynamic.Core;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface ITransSaleRepository
    {
        public Task<List<TransSale>> GetAll(FilterTransSaleRequest param);

        public Task<List<TransSale>> GetBySaleGroup(Guid saleGroup, FilterModel param);
        public Task<List<TransSale>> GetBySaleGroup(Guid saleGroup);

        public Task<TransSale> GetById(Guid id);

        public Task<TransSale> GetByUsername(string username);

        public Task<TransSale> GetByRefCode(string refCode);

        public Task Create(TransSale param);

        public Task Update(TransSale param);

        public Task BulkUpdate(List<TransSale> param);

        public Task<bool> DuplicateKey(TransSale param, MethodType method);

        public Task<int> CountAllAsync();

        public Task<bool> VerifyRefCode(string RefCode);
        Task<List<TransSale>> GetAll();
         Task<List<TransSale>> GetBySaleGroup(List<Guid> saleGroup);
    }

    public class TransSaleRepository : ITransSaleRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public TransSaleRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<TransSale>> GetAll(FilterTransSaleRequest param)
        {
            List<TransSale> output = new List<TransSale>();

            try
            {
                var queryable = _context.TransSales.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();
                    queryable = queryable
                        .Where(x =>
                        x.Username.ToLower().Contains(param.TextSearch) ||
                        x.FullName.ToLower().Contains(param.TextSearch) ||
                        (x.NickName ?? "").ToLower().Contains(param.TextSearch)
                    ).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Username))
                {
                    queryable = queryable.Where(x => x.Username != null).AsQueryable();

                    queryable = queryable.Where(x => x.Username.ToLower() == param.Username.ToLower()).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.FullName))
                {
                    queryable = queryable.Where(x => x.FullName != null).AsQueryable();

                    queryable = queryable.Where(x => x.FullName.ToLower() == param.FullName.ToLower()).AsQueryable();
                }

                if (param.IsActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();
                }

                #endregion

                string sortOrder = string.Empty;

                if (!string.IsNullOrEmpty(param.SortName))
                {
                    sortOrder = param.SortName;
                }
                else
                {
                    sortOrder = "CreatedDate";
                }

                if (!string.IsNullOrEmpty(param.SortType))
                {
                    sortOrder += $" {param.SortType}";
                }
                else
                {
                    sortOrder += " desc";
                }

                output = await queryable.OrderBy(sortOrder).AsNoTracking().Include(a => a.MasterSaleGroup).ToListAsync();

                var cacheKey = "TransSalesCount";

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

        public async Task<List<TransSale>> GetBySaleGroup(Guid saleGroup, FilterModel param)
        {
            List<TransSale> output = new List<TransSale>();

            try
            {
                var queryable = _context.TransSales.AsQueryable();

                #region Filter Data Zone

                queryable = queryable.Where(x => x.MasterSaleGroupId == saleGroup).AsQueryable();


                #endregion

                output = await queryable.AsNoTracking().ToListAsync();

                var cacheKey = "TransSalesCount";

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

        public async Task<TransSale> GetById(Guid id)
        {
            TransSale? Outbound = new TransSale();

            try
            {
                var queryable = _context.TransSales.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<TransSale> GetByUsername(string username)
        {
            TransSale? Outbound = new TransSale();

            try
            {
                var queryable = _context.TransSales.Where(x => x.Username.ToLower() == username.ToLower()).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<TransSale> GetByRefCode(string refCode)
        {
            TransSale? Outbound = new TransSale();

            try
            {
                var queryable = _context.TransSales.Where(x => x.RefCode.ToLower() == refCode.ToLower()).AsQueryable();

                queryable = queryable.Include(x => x.MasterSaleGroup);

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(TransSale param)
        {
            try
            {
                _context.TransSales.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(TransSale param)
        {
            try
            {
                _context.TransSales.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task BulkUpdate(List<TransSale> param)
        {
            try
            {
                _context.TransSales.UpdateRange(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(TransSale param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.TransSales
                    .Where(x => x.Username.ToLower() == param.Username.ToLower())
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.TransSales
                    .Where(x => x.Username.ToLower() == param.Username.ToLower() &&
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

        public async Task<bool> VerifyRefCode(string refCode)
        {
            bool result = false;

            try
            {
                result = await _context.TransSales
                    .Where(x => x.RefCode.ToLower() == refCode.ToLower())
                    .AnyAsync();
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
            var cacheKey = "TransSalesCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.TransSales.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }

        public async Task<List<TransSale>> GetBySaleGroup(Guid saleGroup)
        {
            return await _context.TransSales.Where(a=> a.MasterSaleGroupId == saleGroup).ToListAsync();
        }

        public async Task<List<TransSale>> GetAll()
        {
            return await _context.TransSales.Where(a => a.IsActive).ToListAsync();
        }

        public async Task<List<TransSale>> GetBySaleGroup(List<Guid> saleGroup)
        {
            return await _context.TransSales.Where(a => saleGroup.Contains(a.MasterSaleGroupId)).ToListAsync();
        }
    }
}

