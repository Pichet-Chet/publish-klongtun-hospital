using KTH.MODELS.Custom.Request.Finance;
using KTH.MODELS.Custom.Request.TransClosePeriodIncomeHeader;
using KTH.MODELS.Custom.Request.TransOrder;
using KTH.MODELS.Custom.Request.TransPayment;
using KTH.REPOSITORIES.Dto;
using KTH.REPOSITORIES.Model.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Dynamic.Core;
using static KTH.MODELS.Constants.ConstantsMassage;

namespace KTH.REPOSITORIES
{
    #region | Interface |
    public interface IFinanceRepository
    {
        public Task<List<TransOrder>> GetOrderFilter(FilterTransOrderRequest param);
        public Task CreateOrder(TransOrder transOrder);
        public Task UpdateOrder(List<TransOrderItem> newTransOrderItem, List<TransOrderItem> oldTransOrderItem);
        public Task<int> CountOrderAllAsync();
        public Task<int> CountTransPaymentAllAsync();
        Task<List<TransPaymentHeader>> GetTransPaymentFilter(GetTransPaymentFilterRequest param);
        Task<TransPaymentHeader> GetTransPaymentWithId(Guid id);
        Task<List<TransPaymentHeader>> GetTransPaymentWithTransOrderId(Guid transOrderId);
        Task CreateTransPayment(TransPaymentHeader transPaymentHeader);
        Task<TransOrder> GetOrderWithId(Guid orderId);
        Task<List<TransOrderItem>> GetTransOrderItemWithTransOrderId(Guid transOrderId);
        Task<bool> ValidateOrderStatusORx05WithTransCaseId(Guid transCaseId);
        Task<bool> ValidatePaymentUnsuccess(Guid transCaseId, Guid transOrderId);
        Task UpdateTransOrderApprove();
        Task<List<TransPaymentHeader>> GetTransPaymentWithCaseId(Guid transCaseId);
        Task UpdateTransPayment();
        Task<bool> CheckTransCaseOpenRefund(Guid transCaseId);
        Task<List<TransOrder>> GetTransOrderWithCaseId(Guid transCaseId);
        Task<List<TransPaymentHeader>> GetTransPaymentHeaderForAccount(GetAccountRefundFilterRequest param);
        Task<bool> CheckTransCaseOpenOrder(Guid transCaseId);
        Task<GetAccountRefundFinanceFilterModel> GetAccountRefundFinanceTypeReceiveFilter(AccountRefundFinanceRequest param);
        Task<GetAccountRefundFinanceFilterModel> GetAccountRefundFinanceTypeRefundFilter(AccountRefundFinanceRequest param);
        Task<AccountRefundFinance1663Model> AccountRefundFinance1663(AccountRefundFinance1663Request param, bool isExcel = false);

        Task CreateTransCaseRefundOverdue1663(TransCaseRefundOverdue1663 data);

        Task<TransPaymentHeader> GetTransPaymentLastDate(int BucketType);

        Task<List<TransCaseRefundOverdue1663>> GetTransCaseRefundOverdue1663(GetAccountRefund1663FilterRequest param);
        Task<TransCaseRefundOverdue1663> GetTransCaseRefundOverdue1663WithId(Guid id);
        Task<List<TransCaseRefundOverdue1663>> GetTransCaseRefundOverdue1663NotClosePeriod();
        Task<List<TransCaseRefundOverdue1663>> GetTransCaseRefundOverdue1663WithPeriodIncomeHeaderId(Guid headerId);

        Task UpdateTransCaseRefundOverdue1663();

        Task<GetPaymentAndRefundModel> GetPaymentAndRefund(PaymentAndRefundRequest param);

        #region Summary 

        Task<List<TransClosePeriodIncomeHeader>> GetIncomeHeaders(FilterTransClosePeriodIncomeHeaderRequest param);

        Task<TransClosePeriodIncomeHeader> GetIncomeHeadersById(Guid Id);

        #endregion
    }
    #endregion

    #region | Class |
    public class FinanceRepository : IFinanceRepository
    {
        private readonly KthContext _context;
        private readonly IMemoryCache _cache;
        public FinanceRepository(KthContext context, IMemoryCache cache)
        {

            _context = context;

            _cache = cache;
        }

        public async Task<bool> CheckTransCaseOpenRefund(Guid transCaseId)
        {
            return await _context.TransPaymentHeaders.AnyAsync(a => a.TransCaseId.Equals(transCaseId) && a.TypePaymet == TextFix.PaymentTypeRefund);
        }

        public async Task<bool> CheckTransCaseOpenOrder(Guid transCaseId)
        {
            return await _context.TransPaymentHeaders.AnyAsync(a => a.TransCaseId.Equals(transCaseId) && a.TypePaymet == TextFix.PaymentTypeReceive);
        }

        public async Task<int> CountOrderAllAsync()
        {
            var cacheKey = "TransOrdersCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.TransOrders.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }

        public async Task<int> CountTransPaymentAllAsync()
        {
            var cacheKey = "TransPaymentCount";
            if (!_cache.TryGetValue(cacheKey, out int count))
            {
                count = await _context.TransPaymentHeaders.CountAsync();

                // Set cache options and expiration as needed
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                _cache.Set(cacheKey, count, cacheOptions);
            }

            return count;
        }

        public async Task CreateOrder(TransOrder transOrder)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.TransOrders.AddAsync(transOrder);

                    await _context.TransOrderItems.AddRangeAsync(transOrder.TransOrderItems);

                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                }
                catch
                {
                    await transaction.RollbackAsync();

                    throw;
                }
            }
        }

        public async Task CreateTransPayment(TransPaymentHeader transPaymentHeader)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    await _context.TransPaymentHeaders.AddAsync(transPaymentHeader);
                    await _context.TransPaymentDetails.AddRangeAsync(transPaymentHeader.TransPaymentDetails);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<List<TransOrder>> GetOrderFilter(FilterTransOrderRequest param)
        {
            List<TransOrder> output = new List<TransOrder>();

            try
            {
                var queryable = _context.TransOrders.Where(a => a.IsActive).AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                if (!string.IsNullOrEmpty(param.TransCaseId))
                {
                    queryable = queryable.Where(x => x.TransCaseId.Equals(param.TransCaseId.ToGuid())).AsQueryable();
                }
                var test = await queryable.CountAsync();

                if (!string.IsNullOrEmpty(param.MasterStatusCode))
                {
                    queryable = queryable.Where(x => x.MasterStatusCode == param.MasterStatusCode).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.CreatedBySysRoleCode))
                {
                    queryable = queryable.Where(x => x.CreatedBySysRoleCode == param.CreatedBySysRoleCode).AsQueryable();
                }

                #endregion

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

                output = await queryable.OrderBy(sortOrder).Include(a => a.TransCase).Include(a => a.TransOrderItems).Include(a => a.TransPaymentHeaders).Include(a => a.MasterStatusCodeNavigation).AsNoTracking().ToListAsync();

                var cacheKey = "TransOrdersCount";

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

        public async Task<TransOrder> GetOrderWithId(Guid orderId)
        {
            try
            {
                return await _context.TransOrders.Include(a => a.TransOrderItems).Include(a => a.TransPaymentHeaders).Include(a => a.MasterStatusCodeNavigation).Include(a => a.CreatedBySysRoleCodeNavigation).FirstOrDefaultAsync(a => a.Id.Equals(orderId) && a.IsActive);
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<TransOrderItem>> GetTransOrderItemWithTransOrderId(Guid transOrderId)
        {
            try
            {
                return await _context.TransOrderItems.Where(a => a.TransOrderId.Equals(transOrderId) && a.IsActive).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<TransOrder>> GetTransOrderWithCaseId(Guid transCaseId)
        {
            return await _context.TransOrders.Where(a => a.TransCaseId.Equals(transCaseId)).Include(a => a.TransOrderItems).Include(a => a.TransPaymentHeaders).ThenInclude(a => a.TransPaymentDetails).ToListAsync();
        }

        public async Task<List<TransPaymentHeader>> GetTransPaymentFilter(GetTransPaymentFilterRequest param)
        {
            try
            {
                var transPayment = _context.TransPaymentHeaders.Where(a => a.IsActive).AsQueryable();
                if (!string.IsNullOrEmpty(param.TextSearch))
                {
                    transPayment = transPayment.Where(a => a.TransactionNo.Contains(param.TextSearch) || a.TransCaseNo.Contains(param.TextSearch) || a.CreatedBy.Contains(param.TextSearch)).AsQueryable();
                }
                if (param.StartDate != null)
                {
                    transPayment = transPayment.Where(a => a.CreatedDate >= param.StartDate).AsQueryable();
                }
                if (param.EndDate != null)
                {
                    transPayment = transPayment.Where(a => a.CreatedDate <= param.StartDate).AsQueryable();
                }
                if (!string.IsNullOrEmpty(param.transCaseId))
                {
                    transPayment = transPayment.Where(a => a.TransCaseId.Equals(param.transCaseId.ToGuid()));
                }
                if (!string.IsNullOrEmpty(param.transClientId))
                {
                    transPayment = transPayment.Where(a => a.TransClientId.Equals(param.transClientId.ToGuid()));
                }

                if (!string.IsNullOrEmpty(param.TypePayment))
                {
                    transPayment = transPayment.Where(a => a.TypePaymet.Equals(param.TypePayment));
                }

                if (param.IsCloseBalance != null)
                {
                    transPayment = transPayment.Where(x => x.IsCloseBalance == param.IsCloseBalance);
                }

                if (param.MoneyBucket != null)
                {
                    transPayment = transPayment.Where(x => x.MoneyBucket == param.MoneyBucket);
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

                var output = await transPayment.OrderBy(sortOrder).Include(a => a.TransPaymentDetails).Include(a => a.TransClient).Include(a => a.TransOrder).Include(a => a.TransCase).ToListAsync();

                var cacheKey = "TransPaymentCount";

                _cache.Set(cacheKey, output.Count());

                #region Pagination

                if (!param.IsAll)
                {
                    output = output
                   .Skip((param.PageNumber - 1) * param.PageSize)
                   .Take(param.PageSize)
                   .ToList();
                }

                #endregion

                return output;
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<TransPaymentHeader>> GetTransPaymentHeaderForAccount(GetAccountRefundFilterRequest param)
        {
            var query = _context.TransPaymentHeaders.Where(a => a.IsActive).Include(a => a.TransPaymentDetails).ThenInclude(a => a.TransOrderItem != null && a.TransOrderItem.Reserve > 0).AsNoTracking().AsQueryable();
            if (param.StartActionDate != null)
            {
                query = query.Where(a => a.CreatedDate >= param.StartActionDate);
            }
            if (param.EndActionDate != null)
            {
                query = query.Where(a => a.CreatedDate <= param.EndActionDate);
            }
            if (param.StartActionRefund != null)
            {
                query = query.Where(a => a.AccountingRefundDate >= param.StartActionRefund);
            }
            if (param.EndActionRefund != null)
            {
                query = query.Where(a => a.AccountingRefundDate <= param.EndActionRefund);
            }
            if (!string.IsNullOrEmpty(param.TextSearch))
            {
                query = query.Where(a => a.TransactionNo.Contains(param.TextSearch));
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
                return await query.Include(a => a.TransCase).ThenInclude(a => a.TransConsultRoom).Include(a => a.AccountingRefundByNavigation).Include(a => a.TransClient).ToListAsync();
            }
            else
            {
                return await query.Include(a => a.TransCase).ThenInclude(a => a.TransConsultRoom).Include(a => a.AccountingRefundByNavigation).Include(a => a.TransClient).Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToListAsync();
            }
        }

        public async Task<List<TransPaymentHeader>> GetTransPaymentWithCaseId(Guid transCaseId)
        {
            return await _context.TransPaymentHeaders.Where(a => a.TransCaseId.Equals(transCaseId) && a.IsActive).Include(a => a.TransPaymentDetails).ToListAsync();
        }

        public async Task<TransPaymentHeader> GetTransPaymentWithId(Guid id)
        {
            try
            {
                return await _context.TransPaymentHeaders.Include(a => a.TransPaymentDetails).Include(a => a.TransPaymentDetails).FirstOrDefaultAsync(a => a.Id.Equals(id));
            }
            catch
            {
                throw;
            }
        }

        public async Task<List<TransPaymentHeader>> GetTransPaymentWithTransOrderId(Guid transOrderId)
        {
            try
            {
                return await _context.TransPaymentHeaders.Where(a => a.TransOrderId.Equals(transOrderId)).Include(a => a.TransPaymentDetails).ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateTransPayment()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrder(List<TransOrderItem> newTransOrderItem, List<TransOrderItem> oldTransOrderItem)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.TransOrderItems.RemoveRange(oldTransOrderItem);
                    await _context.TransOrderItems.AddRangeAsync(newTransOrderItem);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task UpdateTransOrderApprove()
        {
            await _context.SaveChangesAsync();
        }


        public async Task<bool> ValidateOrderStatusORx05WithTransCaseId(Guid transCaseId)
        {
            return await _context.TransOrders.AsNoTracking().AnyAsync(a => a.TransCaseId.Equals(transCaseId) && a.IsActive && a.MasterStatusCode.ToLower() == StatusFlowCase.ORx05.Key.ToLower());
        }

        public async Task<bool> ValidatePaymentUnsuccess(Guid transCaseId, Guid transOrderId)
        {
            return await _context.TransOrders.AsNoTracking().AnyAsync(a => a.TransCaseId.Equals(transCaseId) 
                                                                        && a.IsActive && a.MasterStatusCode.ToLower() != StatusFlowCase.ORx02.Key.ToLower() 
                                                                        && a.MasterStatusCode.ToLower() != StatusFlowCase.ORx04.Key.ToLower()
                                                                        && a.MasterStatusCode.ToLower() != StatusFlowCase.ORx99.Key.ToLower());
        }

        public async Task<List<TransClosePeriodIncomeHeader>> GetIncomeHeaders(FilterTransClosePeriodIncomeHeaderRequest param)
        {
            List<TransClosePeriodIncomeHeader> output = new List<TransClosePeriodIncomeHeader>();

            try
            {
                var queryable = _context.TransClosePeriodIncomeHeaders.AsQueryable();

                #region Filter Data Zone

                param.TrimAllProperties();

                #endregion

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

                output = await queryable.OrderBy(sortOrder).AsNoTracking().Include(x => x.TransClosePeriodIncomeDetails).ThenInclude(x => x.TransPaymentHeaderId).ToListAsync();

                var cacheKey = "MasterHospitalsCount";

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

        public async Task<TransClosePeriodIncomeHeader> GetIncomeHeadersById(Guid Id)
        {
            TransClosePeriodIncomeHeader? Outbound = new TransClosePeriodIncomeHeader();

            try
            {
                var queryable = _context.TransClosePeriodIncomeHeaders.Where(x => x.Id == Id).AsQueryable();

                Outbound = await queryable.AsNoTracking().Include(x => x.TransClosePeriodIncomeDetails).ThenInclude(x => x.TransPaymentHeader).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return Outbound;
        }

        public async Task<GetAccountRefundFinanceFilterModel> GetAccountRefundFinanceTypeReceiveFilter(AccountRefundFinanceRequest param)
        {
            GetAccountRefundFinanceFilterModel result = new GetAccountRefundFinanceFilterModel();
            var query = _context.TransPaymentHeaders.Where(a => a.IsActive && a.TransOrder.TransOrderItems.Any(a => a.Reserve > 0)).Include(a => a.TransCase)
                    .ThenInclude(a => a.TransConsultRoom)
                    .Include(a => a.TransClient)
                    .Include(a => a.AccountingRefundByNavigation)
                    .Include(a => a.TransOrder)
                    .ThenInclude(a => a.TransOrderItems).AsQueryable();

            if (param.StartDate != null)
            {
                query = query.Where(a => a.CreatedDate >= param.StartDate);
            }

            if (param.EndDate != null)
            {
                query = query.Where(a => a.CreatedDate <= param.EndDate);
            }

            if (!string.IsNullOrEmpty(param.Status))
            {
                query = query.Where(a => a.TypePaymet.ToLower() == param.Status.ToLower());
            }

            if (!string.IsNullOrEmpty(param.TextSearch))
            {
                query = query.Where(a => a.TransClient.FullName.ToLower().Contains(param.TextSearch.ToLower()));
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

            result.Count = await query.CountAsync();
            if (param.IsAll)
            {
                result.TransPaymentHeaderList = await query.OrderBy(sortOrder).ToListAsync();

            }
            else
            {
                result.TransPaymentHeaderList = await query
                    .OrderBy(sortOrder)
                    .Skip((param.PageNumber - 1) * param.PageSize)
                    .Take(param.PageSize).ToListAsync();
            }

            return result;
        }

        public async Task<GetAccountRefundFinanceFilterModel> GetAccountRefundFinanceTypeRefundFilter(AccountRefundFinanceRequest param)
        {
            GetAccountRefundFinanceFilterModel result = new GetAccountRefundFinanceFilterModel();
            var query = _context.TransPaymentHeaders.Where(a => a.IsActive && a.TypePaymet == TextFix.PaymentTypeRefund && (a.IsReceipt || (!a.IsReceipt && !a.IsCloseBalance))).Include(a => a.TransCase)
                    .ThenInclude(a => a.TransConsultRoom)
                    .Include(a => a.TransClient)
                    .Include(a => a.AccountingRefundByNavigation)
                    .Include(a => a.TransOrder)
                    .ThenInclude(a => a.TransOrderItems).AsQueryable();

            if (param.StartDate != null)
            {
                query = query.Where(a => a.CreatedDate >= param.StartDate);
            }

            if (param.EndDate != null)
            {
                query = query.Where(a => a.CreatedDate <= param.EndDate);
            }

            if (!string.IsNullOrEmpty(param.Status))
            {
                query = query.Where(a => a.TypePaymet.ToLower() == param.Status.ToLower());
            }

            if (!string.IsNullOrEmpty(param.TextSearch))
            {
                query = query.Where(a => a.TransClient.FullName.ToLower().Contains(param.TextSearch.ToLower()));
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

            result.Count = await query.CountAsync();
            if (param.IsAll)
            {
                result.TransPaymentHeaderList = await query.OrderBy(sortOrder).ToListAsync();
            }
            else
            {
                result.TransPaymentHeaderList = await query
                 .OrderBy(sortOrder)
                 .Skip((param.PageNumber - 1) * param.PageSize)
                 .Take(param.PageSize).ToListAsync();
            }

            return result;
        }

        public async Task<AccountRefundFinance1663Model> AccountRefundFinance1663(AccountRefundFinance1663Request param, bool isExcel = false)
        {
            var result = new AccountRefundFinance1663Model();
            var query = _context.TransCases.Where(a => a.CreatedDate.Month == param.Month
                                                    && a.CreatedDate.Year == param.Year
                                                    && a.TransOrders.Any(b => b.TransOrderItems.Any(c => c.X1663Paid > 0))
                                                    && a.IsActive)
                        .Include(a => a.TransClient)
                        .Include(a => a.TransOrders)
                        .ThenInclude(a => a.TransOrderItems)
                        .ThenInclude(a => a.MasterItemOrder)
                        .Include(a => a.TransConsultRoom)
                        .ThenInclude(a => a.MasterReferralFrom)
                        .Include(a => a.TransSale)
                        .AsNoTracking()
                        .AsQueryable();


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

            var countTask = await query.CountAsync();
            result.CountData = countTask;
            if (isExcel || param.IsAll)
            {
                result.TransCaseList = await query.OrderBy(sortOrder).ToListAsync();
            }
            else
            {
                result.TransCaseList = await query.OrderBy(sortOrder)
                   .Skip((param.PageNumber - 1) * param.PageSize)
                   .Take(param.PageSize)
                   .ToListAsync();
            }

            return result;
        }

        public async Task CreateTransCaseRefundOverdue1663(TransCaseRefundOverdue1663 data)
        {
            await _context.TransCaseRefundOverdue1663s.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task<TransPaymentHeader> GetTransPaymentLastDate(int BucketType)
        {
            TransPaymentHeader transPaymentHeader = new TransPaymentHeader();

            try
            {
                transPaymentHeader = await _context.TransPaymentHeaders
                    .Where(a =>
                    a.MoneyBucket == BucketType &&
                    a.IsCloseBalance == true
                    ).FirstOrDefaultAsync();
            }
            catch
            {
                throw;
            }

            return transPaymentHeader;
        }

        public async Task<List<TransCaseRefundOverdue1663>> GetTransCaseRefundOverdue1663(GetAccountRefund1663FilterRequest param)
        {
            if (param.IsAll)
            {
                return await _context.TransCaseRefundOverdue1663s.Include(a => a.TransStaff).OrderByDescending(a => a.Year).ThenByDescending(a => a.Month).ToListAsync();
            }
            else
            {
                return await _context.TransCaseRefundOverdue1663s.Include(a => a.TransStaff).Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).OrderByDescending(a => a.Year).ThenByDescending(a => a.Month).ToListAsync();
            }

        }

        public async Task<List<TransCaseRefundOverdue1663>> GetTransCaseRefundOverdue1663NotClosePeriod()
        {
            return await _context.TransCaseRefundOverdue1663s.Where(x => x.TransClosePeriodIncomeHeaderId == null).ToListAsync();
        }

        public async Task<TransCaseRefundOverdue1663> GetTransCaseRefundOverdue1663WithId(Guid id)
        {
            return await _context.TransCaseRefundOverdue1663s.FirstOrDefaultAsync(a => a.Id.Equals(id));
        }

        public async Task<List<TransCaseRefundOverdue1663>> GetTransCaseRefundOverdue1663WithPeriodIncomeHeaderId(Guid headerId)
        {
            return await _context.TransCaseRefundOverdue1663s.Where(x => x.TransClosePeriodIncomeHeaderId == headerId).ToListAsync();
        }

        public async Task UpdateTransCaseRefundOverdue1663()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<GetPaymentAndRefundModel> GetPaymentAndRefund(PaymentAndRefundRequest param)
        {
            GetPaymentAndRefundModel result = new GetPaymentAndRefundModel();

            #region | Filter | 
            var query = _context.TransPaymentHeaders.Where(a => a.IsActive)
                        .Include(a => a.TransCase)
                        .ThenInclude(a => a.TransConsultRoom)
                        .Include(a => a.TransClient)
                        .Include(a => a.TransOrder)
                        .ThenInclude(a => a.TransOrderItems).AsQueryable();

            if (param.StartDate != null)
            {
                query = query.Where(a => a.TransactionDate >= param.StartDate.Value.ToDateTime(TimeOnly.MinValue));
            }

            if (param.EndDate != null)
            {
                query = query.Where(a => a.TransactionDate <= param.EndDate.Value.ToDateTime(TimeOnly.MaxValue));
            }

            if (!string.IsNullOrEmpty(param.System))
            {
                query = query.Where(a => a.MoneyBucket == param.System.ToShort());
            }

            if (!string.IsNullOrEmpty(param.Type))
            {
                query = query.Where(a => a.TypePaymet.ToLower() == param.Type.ToLower());
            }

            if (!string.IsNullOrEmpty(param.Status))
            {
                if (param.Status.ToLower() == MasterStatusFilterPaymentAndRefund.StatusCancel)
                {
                    query = query.Where(a => a.MasterStatusCode == StatusFlowCase.CSPx01.Key);
                }
                else
                {
                    query = query.Where(a => a.MasterStatusCode != StatusFlowCase.CSPx01.Key);
                }
            }

            if (!string.IsNullOrEmpty(param.TextSearch))
            {
                query = query.Where(a => a.TransClient.FullName.Contains(param.TextSearch));
            }

            #endregion

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

            result.CountData = await query.CountAsync();
            result.Data = await query.OrderBy(sortOrder).Select(a => new GetPaymentAndRefundModelData
            {
                MoneyBucket = a.MoneyBucket,
                CaseId = a.TransCaseId,
                CaseNo = a.TransCase.CaseNo,
                Cash = a.PaymentCash,
                ClientFullName = a.TransClient.FullName,
                CaseMasterStatusCode = a.TransCase.MasterStatusCode,
                Credit = a.PaymentCredit,
                IsReceipt = a.IsReceipt,
                MasterStatusCode = a.MasterStatusCode,
                Qr = a.PaymentQrCode,
                Remark = a.Remark,
                TotalAmount = a.TotalAmount,
                TransactionId = a.Id,
                TransactionNo = a.TransactionNo,
                TypePayment = a.TypePaymet,
                UpdateBy = a.UpdatedBy,
                UpdateDate = a.UpdatedDate,
                WithDraw = a.TransCase.TransConsultRoom != null ? a.TransCase.TransConsultRoom.Withdraw : false,
                Order = a.TransOrder != null ? new GetPaymentAndRefundModelDataOrder
                {
                    Id = a.TransOrder.Id,
                    OrderItem = a.TransOrder != null && a.TransOrder.TransOrderItems != null ? a.TransOrder.TransOrderItems.Select(b => new GetPaymentAndRefundModelDataOrderItem
                    {
                        Id = b.Id,
                        Reserve = b.Reserve,
                    }).ToList() : null,
                } : new GetPaymentAndRefundModelDataOrder(),
            }).ToListAsync();

            return result;
        }
    }
    #endregion
}
