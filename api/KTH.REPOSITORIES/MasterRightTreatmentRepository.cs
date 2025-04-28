using KTH.MODELS.Custom.Request.MasterRightTreatment;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface IMasterRightTreatmentRepository
    {
        public Task<List<MasterRightTreatment>> GetAll(FilterMasterRightTreatmentRequest param);

        public Task<MasterRightTreatment> GetById(Guid id);

        public Task<MasterRightTreatment> GetByName(string name);

        public Task Create(MasterRightTreatment param);

        public Task Update(MasterRightTreatment param);

        public Task<bool> DuplicateKey(MasterRightTreatment param, MethodType method);

        public Task<int> CountAllAsync();
    }

    public class MasterRightTreatmentRepository : IMasterRightTreatmentRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public MasterRightTreatmentRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<MasterRightTreatment>> GetAll(FilterMasterRightTreatmentRequest param)
        {
            List<MasterRightTreatment> output = new List<MasterRightTreatment>();

            try
            {
                var queryable = _context.MasterRightTreatments.AsQueryable();

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

                var cacheKey = "MasterRightTreatmentCount";

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

        public async Task<MasterRightTreatment> GetById(Guid id)
        {
            MasterRightTreatment? Outbound = new MasterRightTreatment();

            try
            {
                var queryable = _context.MasterRightTreatments.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<MasterRightTreatment> GetByName(string name)
        {
            MasterRightTreatment? Outbound = new MasterRightTreatment();

            try
            {
                var queryable = _context.MasterRightTreatments.Where(x => x.Name == name).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(MasterRightTreatment param)
        {
            try
            {
                _context.MasterRightTreatments.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(MasterRightTreatment param)
        {
            try
            {
                _context.MasterRightTreatments.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(MasterRightTreatment param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.MasterRightTreatments
                    .Where(x => x.Name.ToLower() == param.Name.ToLower())
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.MasterRightTreatments
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
            var cacheKey = "MasterRightTreatmentCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.MasterRightTreatments.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }
    }
}

