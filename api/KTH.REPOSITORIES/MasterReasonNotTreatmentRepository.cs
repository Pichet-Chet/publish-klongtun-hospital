using KTH.MODELS.Custom.Request.MasterReasonNotTreatment;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface IMasterReasonNotTreatmentRepository
    {
        public Task<List<MasterReasonNotTreatment>> GetAll(FilterMasterReasonNotTreatmentRequest param);

        public Task<MasterReasonNotTreatment> GetById(Guid id);

        public Task<MasterReasonNotTreatment> GetByName(string name);

        public Task Create(MasterReasonNotTreatment param);

        public Task Update(MasterReasonNotTreatment param);

        public Task<bool> DuplicateKey(MasterReasonNotTreatment param, MethodType method);

        public Task<int> CountAllAsync();
    }

    public class MasterReasonNotTreatmentRepository : IMasterReasonNotTreatmentRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public MasterReasonNotTreatmentRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<MasterReasonNotTreatment>> GetAll(FilterMasterReasonNotTreatmentRequest param)
        {
            List<MasterReasonNotTreatment> output = new List<MasterReasonNotTreatment>();

            try
            {
                var queryable = _context.MasterReasonNotTreatments.AsQueryable();

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

                var cacheKey = "MasterReasonNotTreatmentCount";

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

        public async Task<MasterReasonNotTreatment> GetById(Guid id)
        {
            MasterReasonNotTreatment? Outbound = new MasterReasonNotTreatment();

            try
            {
                var queryable = _context.MasterReasonNotTreatments.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<MasterReasonNotTreatment> GetByName(string name)
        {
            MasterReasonNotTreatment? Outbound = new MasterReasonNotTreatment();

            try
            {
                var queryable = _context.MasterReasonNotTreatments.Where(x => x.Name == name).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(MasterReasonNotTreatment param)
        {
            try
            {
                _context.MasterReasonNotTreatments.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(MasterReasonNotTreatment param)
        {
            try
            {
                _context.MasterReasonNotTreatments.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(MasterReasonNotTreatment param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.MasterReasonNotTreatments
                    .Where(x => x.Name.ToLower() == param.Name.ToLower())
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.MasterReasonNotTreatments
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
            var cacheKey = "MasterReasonNotTreatmentCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.MasterReasonNotTreatments.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }
    }
}

