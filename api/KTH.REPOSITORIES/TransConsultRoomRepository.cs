using System;
using KTH.MODELS;
using KTH.MODELS.Custom.Request.TransConsultRoom;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{

    public interface ITransConsultRoomRepository
    {
        public Task<TransConsultRoom> GetByCaseId(Guid caseId);

        public Task Create(TransConsultRoom param);

        public Task Update(TransConsultRoom param);

        public Task<bool> DuplicateKey(TransConsultRoom param, MethodType method);

        public Task<int> CountAllAsync();
    }

    public class TransConsultRoomRepository : ITransConsultRoomRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public TransConsultRoomRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<TransConsultRoom> GetByCaseId(Guid caseID)
        {
            TransConsultRoom? Outbound = new TransConsultRoom();

            try
            {
                var queryable = _context.TransConsultRooms.Where(x => x.TransCaseId == caseID).AsQueryable();

                Outbound = await queryable.AsNoTracking().Include(x => x.MasterGestationalAge).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(TransConsultRoom param)
        {
            try
            {
                _context.TransConsultRooms.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(TransConsultRoom param)
        {
            try
            {
                _context.TransConsultRooms.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> DuplicateKey(TransConsultRoom param, MethodType method)
        {
            bool result = false;

            try
            {
                if (method == MethodType.CREATE)
                {
                    result = await _context.TransConsultRooms
                    .Where(x =>
                    x.TransCaseId == param.TransCaseId
                    )
                    .AnyAsync();
                }
                else if (method == MethodType.UPDATE)
                {
                    result = await _context.TransConsultRooms
                    .Where(x =>
                    x.TransCaseId == param.TransCaseId &&
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
            var cacheKey = "TransConsultRoomsCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.TransConsultRooms.CountAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }
    }

}

