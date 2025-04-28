using KTH.MODELS.Custom.Request.TransAssignAssistantManager;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface ITransAssignAssistantManagerRepository
    {
        public Task<List<TransAssignAssistantManager>> GetAll(FilterTransAssignAssistantManagerRequest param);

        public Task<TransAssignAssistantManager> GetById(Guid id);

        public Task<TransAssignAssistantManager> GetByName(string name);

        public Task Create(TransAssignAssistantManager param);

        public Task Update(TransAssignAssistantManager param);

        //public Task<bool> DuplicateKey(TransAssignAssistantManager param, MethodType method);

        public Task<int> CountAllAsync();
    }

    public class TransAssignAssistantManagerRepository : ITransAssignAssistantManagerRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public TransAssignAssistantManagerRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<TransAssignAssistantManager>> GetAll(FilterTransAssignAssistantManagerRequest param)
        {
            List<TransAssignAssistantManager> output = new List<TransAssignAssistantManager>();

            try
            {
                var queryable = _context.TransAssignAssistantManagers.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();
                    queryable = queryable
                        .Where(x =>
                        x.StaffName.ToLower().Contains(param.TextSearch)
                    ).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.StaffName))
                {
                    queryable = queryable.Where(x => x.StaffName != null).AsQueryable();

                    queryable = queryable.Where(x => x.StaffName.ToLower() == param.StaffName.ToLower()).AsQueryable();
                }

                if (param.IsActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();
                }

                #endregion

                output = await queryable.AsNoTracking().ToListAsync();

                var cacheKey = "TransAssignAssistantManagerCount";

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

        public async Task<TransAssignAssistantManager> GetById(Guid id)
        {
            TransAssignAssistantManager? Outbound = new TransAssignAssistantManager();

            try
            {
                var queryable = _context.TransAssignAssistantManagers.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<TransAssignAssistantManager> GetByName(string name)
        {
            TransAssignAssistantManager? Outbound = new TransAssignAssistantManager();

            try
            {
                var queryable = _context.TransAssignAssistantManagers.Where(x => x.StaffName == name).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(TransAssignAssistantManager param)
        {
            try
            {
                _context.TransAssignAssistantManagers.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(TransAssignAssistantManager param)
        {
            try
            {
                _context.TransAssignAssistantManagers.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        //public async Task<bool> DuplicateKey(TransAssignAssistantManager param, MethodType method)
        //{
        //    bool result = false;

        //    try
        //    {
        //        if (method == MethodType.CREATE)
        //        {
        //            result = await _context.TransAssignAssistantManagers
        //            .Where(x => x.Name.ToLower() == param.Name.ToLower())
        //            .AnyAsync();
        //        }
        //        else if (method == MethodType.UPDATE)
        //        {
        //            result = await _context.TransAssignAssistantManagers
        //            .Where(x => x.Name.ToLower() == param.Name.ToLower() &&
        //            x.Id != param.Id)
        //            .AnyAsync();
        //        }
        //    }
        //    catch
        //    {
        //        throw;
        //    }

        //    return result;
        //}

        public async Task<int> CountAllAsync()
        {
            // Use a caching library like MemoryCache, Redis, etc.
            var cacheKey = "TransAssignAssistantManagerCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.TransAssignAssistantManagers.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }
    }
}

