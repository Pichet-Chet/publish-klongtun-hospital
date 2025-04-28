using KTH.MODELS.Custom.Request.TransCaseCancel;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface ITransCaseCancelRepository
    {
        public Task<List<TransCaseCancel>> GetAll(FilterTransCaseCancelRequest param);

        public Task<TransCaseCancel> GetByCaseId(Guid caseId);

        public Task<TransCaseCancel> GetById(Guid id);

        public Task<TransCaseCancel> GetByReason(Guid id);

        public Task Create(TransCaseCancel param);

        public Task Update(TransCaseCancel param);

        public Task<bool> DuplicateKey(TransCaseCancel param, MethodType method);

        public Task<int> CountAllAsync();
    }

    public class TransCaseCancelRepository : ITransCaseCancelRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public TransCaseCancelRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<TransCaseCancel>> GetAll(FilterTransCaseCancelRequest param)
        {
            List<TransCaseCancel> output = new List<TransCaseCancel>();

            try
            {
                var queryable = _context.TransCaseCancels.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();
                    queryable = queryable
                        .Where(x =>
                        (x.Remark ?? "").ToLower().Contains(param.TextSearch)
                    ).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.TransCaseId))
                {
                    queryable = queryable.Where(x => x.TransCaseId == param.TransCaseId.ToGuid()).AsQueryable();
                }


                if (!string.IsNullOrEmpty(param.MasterReasonNotTreatmentId))
                {
                    queryable = queryable.Where(x => x.MasterReasonNotTreatmentId == param.MasterReasonNotTreatmentId.ToGuid()).AsQueryable();
                }

                #endregion

                output = await queryable.AsNoTracking().Include(x => x.MasterReasonNotTreatment).Include(x => x.TransCase).ToListAsync();

                var cacheKey = "TransCaseCancelCount";

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

        public async Task<TransCaseCancel> GetByCaseId(Guid CaseId)
        {
            TransCaseCancel? Outbound = new TransCaseCancel();

            try
            {
                var queryable = _context.TransCaseCancels.Where(x => x.TransCaseId == CaseId).AsQueryable();

                Outbound = await queryable.AsNoTracking().Include(x => x.MasterReasonNotTreatment).Include(x => x.TransCase).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<TransCaseCancel> GetById(Guid id)
        {
            TransCaseCancel? Outbound = new TransCaseCancel();

            try
            {
                var queryable = _context.TransCaseCancels.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().Include(x => x.MasterReasonNotTreatment).Include(x => x.TransCase).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<TransCaseCancel> GetByReason(Guid id)
        {
            TransCaseCancel? Outbound = new TransCaseCancel();

            try
            {
                var queryable = _context.TransCaseCancels.Where(x => x.MasterReasonNotTreatmentId == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().Include(x => x.MasterReasonNotTreatment).Include(x => x.TransCase).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(TransCaseCancel param)
        {
            try
            {
                _context.TransCaseCancels.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(TransCaseCancel param)
        {
            try
            {
                _context.TransCaseCancels.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(TransCaseCancel param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.TransCaseCancels
                    .Where(x => x.TransCaseId == param.TransCaseId)
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.TransCaseCancels
                    .Where(x => x.TransCaseId == param.TransCaseId &&
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
            var cacheKey = "TransCaseCancelCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.TransCaseCancels.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }
    }
}

