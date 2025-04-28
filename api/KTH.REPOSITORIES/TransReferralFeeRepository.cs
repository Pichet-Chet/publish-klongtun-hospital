using KTH.MODELS.Custom.Request.TransReferralFee;
using KTH.MODELS.Custom.Response.TransReferralFee;
using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using static KTH.MODELS.Constants.ConstantsMassage;

namespace KTH.REPOSITORIES
{
    public interface ITransReferralFeeRepository
    {
        Task AddListAsync(List<TransReferralFee> list);
        Task AddAsync(TransReferralFee transReferralFee);
        Task UpdateAsync();
        Task<int> CountAllAsync();
        Task<int> CountWaitForApproveAsync();
        Task<int> CountCreditAsync();
        Task<int> CountTransSummaryWaitForApproveHeaderAsync();
        Task<List<TransReferralFee>> GetAll();
        Task<List<TransReferralFee>> GetAll(GetTransReferralFilterRequest param);
        Task<TransReferralFee> GetById(Guid id);
        Task AddHistoryAsync(TransReferralHistory transReferral);
        Task<List<TransReferralHistory>> GetHistoryByReferralId(Guid id);
        Task<List<TransReferralFee>> GetTransReferralWaitForApprove(GetTransReferralWaitForApproveRequest param, string[] statusCode);
        Task<List<TransReferralFee>> GetTransReferralCredit(GetTransReferralCreditRequest param);
        Task AddTransSummaryReferral(TransSummaryReferralHeader transSummaryReferralHeader, List<TransSummaryReferralDetail> listTransSummaryReferralDetails);
        Task<List<TransReferralFee>> GetTransReferralCreateSummary(DateTime endDate);
        Task<List<TransReferralFee>> GetByCaseId(Guid id);
        Task<List<TransReferralFee>> GetByCaseId(List<Guid> list);
        Task<List<TransSummaryReferralHeader>> GetTransSummaryReferralHeader(GetTransSummaryReferralRequest param);
        Task<int> CountTransSummaryReferralHeaderAsync();
        Task<TransSummaryReferralHeader> GetSummaryById(Guid id);
        Task DeleteSummaryAsync();
        Task UpdateTransSummaryReferralAsync();
        Task<List<TransSummaryReferralHeader>> GetTransSummaryWaitForApproveHeader(GetSummaryWaitForApproveRequest param);
    }

    public class TransReferralFeeRepository : ITransReferralFeeRepository
    {

        private readonly KthContext _context;
        private readonly IMemoryCache _cache;

        public TransReferralFeeRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task AddAsync(TransReferralFee transReferralFee)
        {
            await _context.TransReferralFees.AddAsync(transReferralFee);
            await _context.SaveChangesAsync();
        }

        public async Task AddListAsync(List<TransReferralFee> list)
        {
            await _context.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TransReferralFee>> GetAll()
        {
            return await _context.TransReferralFees.Where(a => a.IsActive)
                .Include(a => a.ApproverLv1)
                .Include(a => a.ApproverLv2)
                .Include(a => a.Case)
                .Include(a => a.Client)
                .Include(a => a.MasterReferralFrom)
                .Include(a => a.Sale)
                .Include(a => a.TransReferralHistories)
                .Include(a => a.TransSummaryReferralDetails)
                .ToListAsync();
        }

        public async Task<List<TransReferralFee>> GetAll(GetTransReferralFilterRequest param)
        {
            var query = _context.TransReferralFees.Where(a => a.IsActive).AsQueryable();

            if (!string.IsNullOrEmpty(param.SaleId))
            {
                query = query.Where(a => a.SaleId == param.SaleId.ToGuid());
            }

            if (param.TotalPrice != null)
            {
                query = query.Where(a => a.TotalPriceCase >= param.TotalPrice);
            }

            if (param.TotalCredit != null)
            {
                query = query.Where(a => a.Credit >= param.TotalCredit);
            }

            if (!string.IsNullOrEmpty(param.StatusCode))
            {
                query = query.Where(a => a.MasterStatusCode == param.StatusCode);
            }

            if (!string.IsNullOrEmpty(param.TextSearch))
            {
                query = query.Where(a => a.CaseNo.Contains(param.TextSearch)
                                       || (a.Client != null && a.Client.FullName.Contains(param.TextSearch))
                                       || a.MasterStatusCode.Contains(param.TextSearch));
            }

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

            if (param.IsAll)
            {
                return await query
                   .OrderBy(sortOrder)
                   .Include(a => a.ApproverLv1)
                   .Include(a => a.ApproverLv2)
                   .Include(a => a.Case)
                   .Include(a => a.Client)
                   .Include(a => a.MasterReferralFrom)
                   .Include(a => a.Sale)
                   .Include(a => a.TransReferralHistories)
                   .Include(a => a.TransSummaryReferralDetails)
                   .ToListAsync();
            }
            else
            {
                return await query
                   .OrderBy(sortOrder)
                   .Include(a => a.ApproverLv1)
                   .Include(a => a.ApproverLv2)
                   .Include(a => a.Case)
                   .Include(a => a.Client)
                   .Include(a => a.MasterReferralFrom)
                   .Include(a => a.Sale)
                   .Include(a => a.TransReferralHistories)
                   .Include(a => a.TransSummaryReferralDetails)
                   .Skip((param.PageNumber - 1) * param.PageSize)
                   .Take(param.PageSize)
                   .ToListAsync();
            }
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAllAsync()
        {
            // Use a caching library like MemoryCache, Redis, etc.
            var cacheKey = "TransReferralFeeCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.TransReferralFees.Where(a => a.IsActive).CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }

        public async Task<TransReferralFee> GetById(Guid id)
        {
            return await _context.TransReferralFees.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddHistoryAsync(TransReferralHistory transReferral)
        {
            await _context.TransReferralHistories.AddAsync(transReferral);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TransReferralHistory>> GetHistoryByReferralId(Guid id)
        {
            return await _context.TransReferralHistories.Where(a => a.TransReferralId == id).OrderByDescending(a => a.CreatedDate).ToListAsync();
        }

        public async Task<List<TransReferralFee>> GetTransReferralWaitForApprove(GetTransReferralWaitForApproveRequest param, string[] statusCode)
        {
            var query = _context.TransReferralFees.Where(a => a.IsActive && statusCode.Contains(a.MasterStatusCode)).AsQueryable();


            if (!string.IsNullOrEmpty(param.TextSearch))
            {
                query = query.Where(a => a.CaseNo.Contains(param.TextSearch)
                                       || (a.Client != null && a.Client.FullName.Contains(param.TextSearch))
                                       || a.MasterStatusCode.Contains(param.TextSearch));
            }

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

            if (param.IsAll)
            {
                return await query.OrderBy(sortOrder).Include(a => a.ApproverLv1)
                   .Include(a => a.ApproverLv2)
                   .Include(a => a.Case)
                   .Include(a => a.Client)
                   .Include(a => a.MasterReferralFrom)
                   .Include(a => a.Sale)
                   .Include(a => a.TransReferralHistories)
                   .Include(a => a.TransSummaryReferralDetails)
                   .OrderByDescending(a => a.CreatedDate)
                   .ToListAsync();
            }
            else
            {
                return await query.OrderBy(sortOrder).Include(a => a.ApproverLv1)
                   .Include(a => a.ApproverLv2)
                   .Include(a => a.Case)
                   .Include(a => a.Client)
                   .Include(a => a.MasterReferralFrom)
                   .Include(a => a.Sale)
                   .Include(a => a.TransReferralHistories)
                   .Include(a => a.TransSummaryReferralDetails)
                   .Skip((param.PageNumber - 1) * param.PageSize)
                   .Take(param.PageSize)
                   .OrderByDescending(a => a.CreatedDate)
                   .ToListAsync();
            }
        }

        public async Task<List<TransReferralFee>> GetTransReferralCredit(GetTransReferralCreditRequest param)
        {
            var query = _context.TransReferralFees.Where(a => a.IsActive && a.Credit > 0).AsQueryable();


            if (!string.IsNullOrEmpty(param.TextSearch))
            {
                query = query.Where(a => a.CaseNo.Contains(param.TextSearch)
                                       || (a.Client != null && a.Client.FullName.Contains(param.TextSearch))
                                       || a.MasterStatusCode.Contains(param.TextSearch));
            }

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

            if (param.IsAll)
            {
                return await query.OrderBy(sortOrder).Include(a => a.ApproverLv1)
                   .Include(a => a.ApproverLv2)
                   .Include(a => a.Case)
                   .Include(a => a.Client)
                   .Include(a => a.MasterReferralFrom)
                   .Include(a => a.Sale)
                   .Include(a => a.TransReferralHistories)
                   .Include(a => a.TransSummaryReferralDetails)
                   .OrderByDescending(a => a.CreatedDate)
                   .ToListAsync();
            }
            else
            {
                return await query.OrderBy(sortOrder).Include(a => a.ApproverLv1)
                   .Include(a => a.ApproverLv2)
                   .Include(a => a.Case)
                   .Include(a => a.Client)
                   .Include(a => a.MasterReferralFrom)
                   .Include(a => a.Sale)
                   .Include(a => a.TransReferralHistories)
                   .Include(a => a.TransSummaryReferralDetails)
                   .Skip((param.PageNumber - 1) * param.PageSize)
                   .Take(param.PageSize)
                   .OrderByDescending(a => a.CreatedDate)
                   .ToListAsync();
            }
        }

        public async Task<int> CountWaitForApproveAsync()
        {
            // Use a caching library like MemoryCache, Redis, etc.
            var cacheKey = "TransReferralFeeWaitForApproveCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                string[] statusCode = [StatusFlowCase.RFFx02.Key, StatusFlowCase.RFFx03.Key, StatusFlowCase.RFFx04.Key];
                count = await _context.TransReferralFees.Where(a => a.IsActive && statusCode.Contains(a.MasterStatusCode)).CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }

        public async Task<int> CountCreditAsync()
        {
            // Use a caching library like MemoryCache, Redis, etc.
            var cacheKey = "TransReferralFeeCreditCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.TransReferralFees.Where(a => a.IsActive && a.Credit > 0).CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }

        public async Task AddTransSummaryReferral(TransSummaryReferralHeader transSummaryReferralHeader, List<TransSummaryReferralDetail> listTransSummaryReferralDetails)
        {
            await _context.TransSummaryReferralHeaders.AddAsync(transSummaryReferralHeader);
            await _context.TransSummaryReferralDetails.AddRangeAsync(listTransSummaryReferralDetails);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TransReferralFee>> GetTransReferralCreateSummary(DateTime endDate)
        {
            return await _context.TransReferralFees.Where(a => !a.IsCompleted && a.StartConsultDate <= endDate).OrderBy(a => a.CreatedDate).ToListAsync();
        }

        public async Task<List<TransReferralFee>> GetByCaseId(Guid id)
        {
            return await _context.TransReferralFees.Where(a => a.IsActive && a.CaseId == id).ToListAsync();
        }

        public async Task<List<TransReferralFee>> GetByCaseId(List<Guid> list)
        {
            return await _context.TransReferralFees.Where(a => a.IsActive && list.Contains(a.CaseId)).ToListAsync();
        }

        public async Task<List<TransSummaryReferralHeader>> GetTransSummaryReferralHeader(GetTransSummaryReferralRequest param)
        {
            var query = _context.TransSummaryReferralHeaders.Where(a => a.IsActive).AsQueryable();

            if (!string.IsNullOrEmpty(param.TextSearch))
            {
                query = query.Where(a => a.SummaryHeaderNo.Contains(param.TextSearch));
            }


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

            if (param.IsAll)
            {
                return await query.OrderBy(sortOrder)
                   .ToListAsync();
            }
            else
            {
                return await query
                   .OrderBy(sortOrder)
                   .Skip((param.PageNumber - 1) * param.PageSize)
                   .Take(param.PageSize)
                   .OrderByDescending(a => a.CreatedDate)
                   .ToListAsync();
            }
        }

        public async Task<int> CountTransSummaryReferralHeaderAsync()
        {
            // Use a caching library like MemoryCache, Redis, etc.
            var cacheKey = "CountTransSummaryReferralHeaderAsync";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.TransSummaryReferralHeaders.Where(a => a.IsActive).CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }

        public async Task<TransSummaryReferralHeader> GetSummaryById(Guid id)
        {
            return await _context.TransSummaryReferralHeaders.Where(a => a.Id == id && a.IsActive)
                        .Include(a => a.TransSummaryReferralDetails)
                        .ThenInclude(a => a.TransReferralFee)
                        .ThenInclude(a => a.Client)
                        .Include(a => a.TransSummaryReferralDetails)
                        .ThenInclude(a => a.TransReferralFee)
                        .ThenInclude(a => a.MasterReferralFrom)
                        .FirstOrDefaultAsync();
        }

        public async Task DeleteSummaryAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTransSummaryReferralAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<TransSummaryReferralHeader>> GetTransSummaryWaitForApproveHeader(GetSummaryWaitForApproveRequest param)
        {
            var query = _context.TransSummaryReferralHeaders.Where(a => a.IsActive
            && (a.MasterStatusCode == StatusFlowCase.SRFFx01.Key || a.MasterStatusCode == StatusFlowCase.SRFFx02.Key || a.MasterStatusCode == StatusFlowCase.SRFFx03.Key)).AsQueryable();

            if (!string.IsNullOrEmpty(param.TextSearch))
            {
                query = query.Where(a => a.SummaryHeaderNo.Contains(param.TextSearch));
            }

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

            if (param.IsAll)
            {
                return await query.OrderBy(sortOrder)
                   .ToListAsync();
            }
            else
            {
                return await query.OrderBy(sortOrder)
                   .Skip((param.PageNumber - 1) * param.PageSize)
                   .Take(param.PageSize)
                   .OrderByDescending(a => a.CreatedDate)
                   .ToListAsync();
            }
        }

        public async Task<int> CountTransSummaryWaitForApproveHeaderAsync()
        {
            // Use a caching library like MemoryCache, Redis, etc.
            var cacheKey = "CountTransSummaryWaitForApproveHeaderAsync";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.TransSummaryReferralHeaders.Where(a => a.IsActive
            && (a.MasterStatusCode == StatusFlowCase.SRFFx01.Key || a.MasterStatusCode == StatusFlowCase.SRFFx02.Key || a.MasterStatusCode == StatusFlowCase.SRFFx03.Key)).CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }
    }
}
