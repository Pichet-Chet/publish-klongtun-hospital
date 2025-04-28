using KTH.MODELS.Custom.Request.MasterReasonNewAppointment;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface IMasterReasonNewAppointmentRepository
    {
        public Task<List<MasterReasonNewAppointment>> GetAll(FilterMasterReasonNewAppointmentRequest param);

        public Task<List<MasterReasonNewAppointment>> GetAll();

        public Task<MasterReasonNewAppointment> GetById(Guid id);

        public Task<MasterReasonNewAppointment> GetByName(string name);

        public Task Create(MasterReasonNewAppointment param);

        public Task Update(MasterReasonNewAppointment param);

        public Task<bool> DuplicateKey(MasterReasonNewAppointment param, MethodType method);

        public Task<int> CountAllAsync();
    }

    public class MasterReasonNewAppointmentRepository : IMasterReasonNewAppointmentRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public MasterReasonNewAppointmentRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<MasterReasonNewAppointment>> GetAll(FilterMasterReasonNewAppointmentRequest param)
        {
            List<MasterReasonNewAppointment> output = new List<MasterReasonNewAppointment>();

            try
            {
                var queryable = _context.MasterReasonNewAppointments.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();
                    queryable = queryable
                        .Where(x =>
                        (x.Name ?? "").ToLower().Contains(param.TextSearch) ||
                        x.Group.ToLower().Contains(param.TextSearch)
                    ).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Group))
                {
                    queryable = queryable.Where(x => x.Group != null).AsQueryable();

                    queryable = queryable.Where(x => x.Group.ToLower() == param.Group.ToLower()).AsQueryable();
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

                var cacheKey = "MasterReasonNewAppointmentCount";

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

        public async Task<MasterReasonNewAppointment> GetById(Guid id)
        {
            MasterReasonNewAppointment? Outbound = new MasterReasonNewAppointment();

            try
            {
                var queryable = _context.MasterReasonNewAppointments.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<List<MasterReasonNewAppointment>> GetByGroup(string group)
        {
            List<MasterReasonNewAppointment> Outbound = new List<MasterReasonNewAppointment>();

            try
            {
                var queryable = _context.MasterReasonNewAppointments.Where(x => x.Group == group).AsQueryable();

                Outbound = await queryable.AsNoTracking().ToListAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<MasterReasonNewAppointment> GetByName(string name)
        {
            MasterReasonNewAppointment? Outbound = new MasterReasonNewAppointment();

            try
            {
                var queryable = _context.MasterReasonNewAppointments.Where(x => x.Name == name).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(MasterReasonNewAppointment param)
        {
            try
            {
                _context.MasterReasonNewAppointments.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(MasterReasonNewAppointment param)
        {
            try
            {
                _context.MasterReasonNewAppointments.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(MasterReasonNewAppointment param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.MasterReasonNewAppointments
                    .Where(x => x.Name.ToLower() == param.Name.ToLower())
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.MasterReasonNewAppointments
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
            var cacheKey = "MasterReasonNewAppointmentCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.MasterReasonNewAppointments.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }

        public async Task<List<MasterReasonNewAppointment>> GetAll()
        {
            return await _context.MasterReasonNewAppointments.Where(a => a.IsActive).ToListAsync();
        }
    }
}

