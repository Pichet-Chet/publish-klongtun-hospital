using KTH.MODELS.Custom.Request.MasterConsultRoom;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface IMasterConsultRoomRepository
    {
        public Task<List<MasterConsultRoom>> GetAll(FilterMasterConsultRoomRequest param);
        public Task<List<MasterConsultRoom>> GetAll(bool isActive);
        public Task<List<MasterConsultRoom>> GetAll();

        public Task<MasterConsultRoom> GetById(Guid id);
        public Task<MasterConsultRoom> GetById(Guid id, bool isActive);

        public Task Create(MasterConsultRoom param);

        public Task Update(MasterConsultRoom param);

        public Task<bool> DuplicateKey(MasterConsultRoom param, MethodType method);

        public Task<int> CountAllAsync();

        public Task<int> CountByTypeAsync(string name); // Allow : General - Manager only

        public Task<MasterConsultRoom> GetByOwner(string ownerId);
    }

    public class MasterConsultRoomRepository : IMasterConsultRoomRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public MasterConsultRoomRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<MasterConsultRoom>> GetAll(FilterMasterConsultRoomRequest param)
        {
            List<MasterConsultRoom> output = new List<MasterConsultRoom>();

            try
            {
                var queryable = _context.MasterConsultRooms.AsQueryable();

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

                if (!string.IsNullOrEmpty(param.Type))
                {
                    queryable = queryable.Where(x => x.Type != null).AsQueryable();

                    queryable = queryable.Where(x => x.Type.ToLower() == param.Type.ToLower()).AsQueryable();
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

                output = await queryable.ToListAsync();

                var cacheKey = "MasterConsultRoomsCount";

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

        public async Task<MasterConsultRoom> GetById(Guid id)
        {
            MasterConsultRoom? Outbound = new MasterConsultRoom();

            try
            {
                var queryable = _context.MasterConsultRooms.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }


        public async Task Create(MasterConsultRoom param)
        {
            try
            {
                _context.MasterConsultRooms.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(MasterConsultRoom param)
        {
            try
            {
                _context.MasterConsultRooms.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(MasterConsultRoom param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.MasterConsultRooms
                    .Where(x =>
                    x.Type.ToLower() == param.Type.ToLower() &&
                    x.Seq == param.Seq
                    )
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.MasterConsultRooms
                    .Where(x =>
                    x.Type.ToLower() == param.Type.ToLower() &&
                    x.Seq == param.Seq &&
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
            var cacheKey = "MasterConsultRoomsCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.MasterConsultRooms.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }

        public async Task<int> CountByTypeAsync(string type)
        {
            int count = 0;

            try
            {
                var get = await _context.MasterConsultRooms.Where(x => x.Type.ToLower() == type.ToLower()).ToListAsync();

                count = get.Count();
            }
            catch
            {
                count = 0;
            }

            return count;
        }

        public async Task<MasterConsultRoom> GetByOwner(string ownerId)
        {
            return await _context.MasterConsultRooms.FirstOrDefaultAsync(a => a.Owner == ownerId && a.IsActive.HasValue && a.IsActive.Value);
        }

        public async Task<List<MasterConsultRoom>> GetAll()
        {
            return await _context.MasterConsultRooms.ToListAsync();
        }

        public async Task<MasterConsultRoom> GetById(Guid id, bool isActive)
        {
            return await _context.MasterConsultRooms.FirstOrDefaultAsync(a => a.Id == id && a.IsActive == isActive);
        }

        public async Task<List<MasterConsultRoom>> GetAll(bool isActive)
        {
            return await _context.MasterConsultRooms.Where(a => a.IsActive.HasValue && a.IsActive.Value == isActive).ToListAsync();
        }
    }
}

