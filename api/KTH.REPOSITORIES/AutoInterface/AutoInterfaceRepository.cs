using KTH.REPOSITORIES.Dto;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using static KTH.MODELS.Constants.ConstantsMassage;

namespace KTH.REPOSITORIES.AutoInterface
{
    public interface IAutoInterfaceRepository
    {
        public Task<bool> DisabledCaseAuto();

        public Task<List<TransCase>> FinishedCaseAuto();
        Task GenerateCaseTest(List<TransCase> listCase, List<TransConsultRoom> listConsultRoom, List<TransOrder> listOrder, List<TransOrderItem> listOrderItem);

    }

    public class AutoInterfaceRepository : IAutoInterfaceRepository
    {
        DateTime _serverTime;

        private readonly KthContext _context;

        private readonly IMemoryCache _cache;


        public AutoInterfaceRepository(KthContext context, IMemoryCache cache)
        {
            _context = context;

            _cache = cache;

            _serverTime = DateTime.Now;
        }

        public async Task<bool> DisabledCaseAuto()
        {
            bool result = false;

            List<TransCase> transCases = new List<TransCase>();

            try
            {

                var queryable = _context.TransCases.AsQueryable();

                var dayOfDisable = await _context.SysConfigurations.Where(x => x.Key == TextKeyConfigSystem.DisableCaseAuto).FirstOrDefaultAsync();

                #region Filter Data Zone

                queryable = queryable.Where(x => x.CreatedDate.AddDays(Convert.ToInt32(dayOfDisable.Value)) <= DateTime.Now).AsQueryable();

                queryable = queryable.Where(x => x.MasterStatusCode == StatusFlowCase.Register.Key).AsQueryable();

                #endregion

                transCases = await queryable.AsNoTracking().ToListAsync();

                if (transCases != null && transCases.Count > 0)
                {
                    foreach (var item in transCases)
                    {
                        item.MasterStatusCode = StatusFlowCase.SYSx03.Key;
                    }

                    _context.UpdateRange(transCases);

                    await _context.SaveChangesAsync();
                }

                #region Pagination

                #endregion
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<List<TransCase>> FinishedCaseAuto()
        {
            List<TransCase> result = new List<TransCase>();
            try
            {

                var queryable = _context.TransCases.AsQueryable();

                var dayOfFinish = await _context.SysConfigurations.Where(x => x.Key == TextKeyConfigSystem.FinishCaseHour).FirstOrDefaultAsync();

                #region Filter Data Zone

                queryable = queryable.Where(x => x.MasterStatusCode == StatusFlowCase.FNx01.Key).AsQueryable();
                queryable = queryable.Where(x => (_serverTime - x.StartConsultDate).Value.TotalHours >= Convert.ToInt32(dayOfFinish.Value)).AsQueryable();

                #endregion

                result = await queryable
                    .Include(a => a.TransConsultRoom)
                    .ThenInclude(a => a.MasterReferralFrom)
                    .Include(a => a.TransClient)
                    .Include(a => a.TransSale)
                    .Include(a => a.TransOrders)
                    .ThenInclude(a => a.TransOrderItems)
                    .ThenInclude(a => a.MasterItemOrder).ToListAsync();

                if (result != null && result.Count > 0)
                {
                    foreach (var item in result)
                    {
                        item.MasterStatusCode = StatusFlowCase.SYSx01.Key;
                    }

                    //_context.UpdateRange(result);

                    await _context.SaveChangesAsync();
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task<bool> RevokeRefCode()
        {
            bool result = false;

            List<TransClient> transClients = new List<TransClient>();

            List<TransCase> transCases = new List<TransCase>();

            try
            {

                var queryable = _context.TransClients.AsQueryable();

                var dayOfRevoke = await _context.SysConfigurations.Where(x => x.Key == TextKeyConfigSystem.RevokeRefCode).FirstOrDefaultAsync();

                #region Filter Data Zone

                queryable = queryable.Where(x => x.TranSaleRefCode != TextKeyConfigSystem.NoOtp).AsQueryable();

                #endregion

                transClients = await queryable.AsNoTracking().ToListAsync();

                if (transClients != null && transClients.Count > 0)
                {
                    // continute. . . .
                }
            }
            catch
            {
                throw;
            }

            return result;
        }

        public async Task GenerateCaseTest(List<TransCase> listCase, List<TransConsultRoom> listConsultRoom, List<TransOrder> listOrder, List<TransOrderItem> listOrderItem)
        {
            await _context.TransCases.AddRangeAsync(listCase);
            await _context.TransConsultRooms.AddRangeAsync(listConsultRoom);
            await _context.TransOrders.AddRangeAsync(listOrder);
            await _context.TransOrderItems.AddRangeAsync(listOrderItem);

            await _context.SaveChangesAsync();
        }
    }
}

