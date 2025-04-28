using System;
using KTH.MODELS;
using KTH.MODELS.Custom.Request.MasterNationality;
using KTH.MODELS.Custom.Request.TransClosePeriodIncome;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsPermission;

namespace KTH.REPOSITORIES
{
    public interface ITransClosePeriodIncomeRepository
    {
        public Task<List<TransClosePeriodIncomeHeader>> GetAll(FilterTransClosePeriodIncomeRequest param);

        public Task<TransClosePeriodIncomeHeader> GetById(Guid Id);


        public Task CreateClosePeriod(TransClosePeriodIncomeHeader transClosePeriodIncomeHeader,
            List<TransClosePeriodIncomeDetail> transClosePeriodIncomeDetails,
            List<TransPaymentHeader> transPaymentHeaders,
            List<TransCaseRefundOverdue1663> transCaseRefundOverdue1663s
            );

        public Task<int> CountAllAsync();
    }

    public class TransClosePeriodIncomeRepository : ITransClosePeriodIncomeRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;

        public TransClosePeriodIncomeRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<List<TransClosePeriodIncomeHeader>> GetAll(FilterTransClosePeriodIncomeRequest param)
        {
            List<TransClosePeriodIncomeHeader> output = new List<TransClosePeriodIncomeHeader>();

            try
            {
                var queryable = _context.TransClosePeriodIncomeHeaders.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    param.TextSearch = param.TextSearch.ToLower();

                    queryable = queryable
                        .Where(x =>
                        x.ClosePeriodNo.ToLower().Contains(param.TextSearch) ||
                        x.RoleName.ToLower().Contains(param.TextSearch) ||
                        x.MoneyBucket.ToLower().Contains(param.TextSearch) ||
                        (x.ActionName ?? "").ToLower().Contains(param.TextSearch)
                    ).AsQueryable();
                }

                #endregion

                output = await queryable.AsNoTracking().ToListAsync();

                var cacheKey = "TransClosePeriodIncomesCount";

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


        public async Task CreateClosePeriod(
            TransClosePeriodIncomeHeader transClosePeriodIncomeHeader,
            List<TransClosePeriodIncomeDetail> transClosePeriodIncomeDetails,
            List<TransPaymentHeader> transPaymentHeaders,
            List<TransCaseRefundOverdue1663> transCaseRefundOverdue1663s)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.TransClosePeriodIncomeHeaders.AddAsync(transClosePeriodIncomeHeader);

                    await _context.TransClosePeriodIncomeDetails.AddRangeAsync(transClosePeriodIncomeDetails);

                    _context.TransPaymentHeaders.UpdateRange(transPaymentHeaders);

                    _context.TransCaseRefundOverdue1663s.UpdateRange(transCaseRefundOverdue1663s);

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                }
                catch(Exception ex)
                {
                    await transaction.RollbackAsync();

                    throw;
                }
            }
        }


        public async Task<int> CountAllAsync()
        {
            var cacheKey = "TransClosePeriodIncomesHeaderCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.TransClosePeriodIncomeHeaders.CountAsync();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }

        public async Task<TransClosePeriodIncomeHeader> GetById(Guid Id)
        {
            TransClosePeriodIncomeHeader? Outbound = new TransClosePeriodIncomeHeader();

            try
            {
                var queryable = _context.TransClosePeriodIncomeHeaders.Where(x => x.Id == Id).AsQueryable();

                Outbound = await queryable.AsNoTracking().FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }
    }
}

