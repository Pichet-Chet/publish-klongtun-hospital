using System;
using KTH.MODELS.Custom.Request.TransActionHistory;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface ITransActionHistoryRepository
    {
        public Task<List<TransActionHistory>> GetAll(FilterTransActionHistoryRequest param);

        public Task<TransActionHistory> GetById(Guid id);

        public Task Create(TransActionHistory param);

        public Task Update(TransActionHistory param);
    }

    public class TransActionHistoryRepository : ITransActionHistoryRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public TransActionHistoryRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<TransActionHistory>> GetAll(FilterTransActionHistoryRequest param)
        {
            List<TransActionHistory> output = new List<TransActionHistory>();

            try
            {
                var queryable = _context.TransActionHistories.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();


                if (param.TransCaseId != null)
                {
                    queryable = queryable.Where(x => x.TransCaseId != null).AsQueryable();

                    queryable = queryable.Where(x => x.TransCaseId == param.TransCaseId).AsQueryable();
                }

                #endregion

                output = await queryable.AsNoTracking().ToListAsync();

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

        public async Task<TransActionHistory> GetById(Guid id)
        {
            TransActionHistory? Outbound = new TransActionHistory();

            try
            {
                var queryable = _context.TransActionHistories.Where(x => x.Id == id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task Create(TransActionHistory param)
        {
            try
            {
                _context.TransActionHistories.Add(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task Update(TransActionHistory param)
        {
            try
            {
                _context.TransActionHistories.Update(param);

                await _context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}

