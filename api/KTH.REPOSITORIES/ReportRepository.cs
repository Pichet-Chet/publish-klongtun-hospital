using KTH.MODELS.Custom.Request.Report;
using KTH.REPOSITORIES.Dto;
using KTH.REPOSITORIES.Model.Report;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KTH.REPOSITORIES
{
    public interface IReportRepository
    {
        Task<GetMonthlyAppointmentReportModel> GetMonthlyAppointmentReport(int year, int month);


        Task<List<TransCase>> YearlyCaseReportReport(int year); // Report No.5

        Task<List<TransCase>> DailyCaseByServiceReport(int year, int month); // Report No.6

        Task<List<TransCaseCancel>> MonthlyRefusalCaseReport(int year, int month, string statusCode); // Report No.8

        Task<List<TransCase>> DailyCaseReport(DateOnly date); // Report No.9

        Task<ConsultStaffCaseReportModel> GetConsultStaffCaseReport(ConsultStaffCaseReportRequest param);
        Task<ForeignClientReportModel> GetForeignClientReport(int year, int month);
        Task<CaseByTypeReportModel> GetCaseByTypeReport(int year, int month);
    }

    public class ReportRepository : IReportRepository
    {
        private readonly KthContext _context;
        public ReportRepository(KthContext kthContext)
        {
            _context = kthContext;
        }

        public async Task<ConsultStaffCaseReportModel> GetConsultStaffCaseReport(ConsultStaffCaseReportRequest param)
        {
            ConsultStaffCaseReportModel result = new ConsultStaffCaseReportModel();
            var query = _context.TransCases.Where(a => a.StartConsultDate.Value >= param.StartDate.ToDateTime(TimeOnly.MinValue) && a.StartConsultDate <= param.EndDate.ToDateTime(TimeOnly.MaxValue) && a.IsActive).AsQueryable();
            result.CountData = await query.CountAsync();
            result.Data = await query.Include(a => a.TransOrders).Include(a => a.TransConsultRoom).Select(a => new ConsultStaffCaseReportModelData
            {
                Id = a.Id,
                TransStaffId = a.TransConsultRoom != null ? a.TransConsultRoom.TransStaffId : null,
                MasterStatusCode = a.MasterStatusCode,
                MasterReasonNewAppointmentId = a.MasterReasonNewAppointmentId.HasValue ? a.MasterReasonNewAppointmentId.Value : null,
                ReceiveServiceDate = a.ReceiveServiceDate,
                IsNewAppointment = a.IsNewAppointment.HasValue ? a.IsNewAppointment.Value : false,
                Order = a.TransOrders.Any() ? a.TransOrders.Select(b => new ConsultStaffCaseReportOrder
                {
                    Id = b.Id,
                    OrderItem = b.TransOrderItems.Any() ? b.TransOrderItems.Select(c => new ConsultStaffCaseReportOrderItem
                    {
                        Id = c.Id,
                        MasterOrderItemId = c.MasterItemOrderId,
                    }).ToList() : new List<ConsultStaffCaseReportOrderItem>()
                }).ToList() : new List<ConsultStaffCaseReportOrder>()
            }).ToListAsync();

            return result;
        }

        public async Task<GetMonthlyAppointmentReportModel> GetMonthlyAppointmentReport(int year, int month)
        {
            GetMonthlyAppointmentReportModel result = new GetMonthlyAppointmentReportModel();
            var query = _context.TransCases.Where(a => a.IsNewAppointment.Value && a.StartConsultDate.Value.Month == month && a.StartConsultDate.Value.Year == year && a.IsActive).AsQueryable();
            result.CountData = await query.CountAsync();
            result.Data = await query.Select(a => new GetMonthlyAppointmentReportModelData
            {
                Id = a.Id,
                IsNewAppointment = a.IsNewAppointment.Value,
                MasterReasonNewAppointmentId = a.MasterReasonNewAppointmentId.Value,
                StartConsultDate = a.StartConsultDate.Value,
                MasterStatusCode = a.MasterStatusCode,
            }).ToListAsync();

            return result;
        }

        // Report No.5
        public async Task<List<TransCase>> YearlyCaseReportReport(int year)
        {
            try
            {
                var query = _context.TransCases
                    .Where(a => a.StartConsultDate.Value.Year == year)
                    .Include(x => x.TransOrders)
                    .ThenInclude(x => x.TransOrderItems)
                    .ThenInclude(x => x.MasterItemOrder)
                    .ThenInclude(x => x.MasterItemsOrderGroup).AsQueryable();


                var execute = await query
                    .Include(x => x.TransConsultComments)
                    .Include(x => x.MasterReasonNewAppointment)
                    .Include(x => x.TransClient)
                    .ToListAsync();

                execute = execute
                    .Where(x => x.TransOrders
                        .Any(order => order.TransOrderItems
                            .Any(item =>
                            (item.MasterItemOrder?.MasterItemsOrderGroup?.Code == "0004" ||
                            item.MasterItemOrder?.MasterItemsOrderGroup?.Code == "0005") &&
                            item.Paid == true
                            )))
                    .ToList();

                return execute;
            }
            catch
            {
                throw;
            }
        }

        // Report No.6
        public async Task<List<TransCase>> DailyCaseByServiceReport(int year, int month)
        {
            try
            {
                var query = _context.TransCases.Where(a =>
                  a.StartConsultDate.Value.Month == month &&
                  a.StartConsultDate.Value.Year == year).AsQueryable();

                var execute = await query
                    .Include(x => x.TransConsultComments)
                    .Include(x => x.MasterReasonNewAppointment)
                    .Include(x => x.TransClient)
                    .ToListAsync();
                return execute;
            }
            catch
            {
                throw;
            }
        }

        // Report No.8
        public async Task<List<TransCaseCancel>> MonthlyRefusalCaseReport(int year, int month, string statusCode)
        {
            try
            {
                var query = _context.TransCaseCancels.Where(x => x.CreatedDate != null).AsQueryable();

                query = query.Where(a =>
                   a.CreatedDate.Value.Month == month &&
                   a.CreatedDate.Value.Year == year).AsQueryable();

                var execute = await query
                    .Include(x => x.MasterReasonNotTreatment)
                    .ToListAsync();

                return execute;
            }
            catch
            {
                throw;
            }
        }

        // Report No.9
        public async Task<List<TransCase>> DailyCaseReport(DateOnly date)
        {
            try
            {
                DailyCaseReportModel result = new DailyCaseReportModel();

                var query = _context.TransCases.Where(x => x.ReceiveServiceDate == date &&
                x.IsActive).AsQueryable();

                result.CountData = await query.CountAsync();

                var execute = await query
                    .Include(x => x.TransConsultComments)
                    .Include(x => x.MasterReasonNewAppointment)
                    .Include(x => x.TransClient)
                    .ToListAsync();

                return execute;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ForeignClientReportModel> GetForeignClientReport(int year, int month)
        {
            ForeignClientReportModel result = new ForeignClientReportModel();
            var query = _context.TransCases.Include(a => a.TransClient)
                    .Include(a => a.TransSale)
                    .Include(a => a.MasterSaleGroup)
                    .Include(a => a.TransConsultRoom)
                    .ThenInclude(a => a.MasterGestationalAge)
                    .Include(a => a.TransConsultRoom)
                    .ThenInclude(a => a.MasterReferralFrom)
                    .Include(a => a.TransOrders)
                    .Where(a => a.StartConsultDate.HasValue
                && a.StartConsultDate.Value.Year == year
                && a.StartConsultDate.Value.Month == month
                && a.IsActive
                && a.IsForeigner == true).AsQueryable();

            result.CountData = await query.CountAsync();
            result.Data = await query.Select(a => new ForeignClientReportModelData
            {
                Id = a.Id,
                CaseNo = a.CaseNo,
                ClientName = a.TransClient.FullName,
                ReceiveServiceDate = a.ReceiveServiceDate,
                Mo = a.TransConsultRoom != null ? a.TransConsultRoom.MasterGestationalAge.Name : string.Empty,
                TeamSaleName = a.MasterSaleGroup != null ? a.MasterSaleGroup.Name : string.Empty,
                From = a.TransConsultRoom != null ? a.TransConsultRoom.MasterReferralFrom.Name : string.Empty,
                Order = a.TransOrders != null ? a.TransOrders.Select(b => new ForeignClientReportModelDataOrder
                {
                    Id = b.Id,
                    OrderItem = b.TransOrderItems != null ? b.TransOrderItems.Select(c => new ForeignClientReportModelDataOrderItem
                    {
                        Id = c.Id,
                        MasterItemOrderId = c.MasterItemOrderId,
                    }).ToList() : new List<ForeignClientReportModelDataOrderItem>()
                }).ToList() : new List<ForeignClientReportModelDataOrder>()
            }).ToListAsync();
            return result;
        }

        public async Task<CaseByTypeReportModel> GetCaseByTypeReport(int year, int month)
        {
            CaseByTypeReportModel result = new CaseByTypeReportModel();

            var query = _context.TransCases.Where(a => a.IsActive && a.StartConsultDate.HasValue
                        && a.StartConsultDate.Value.Year == year
                        && a.StartConsultDate.Value.Month == month)
                .Include(a => a.TransConsultRoom)
                .Include(a => a.TransOrders)
                .ThenInclude(a => a.TransOrderItems).AsQueryable();

            result.CountData = await query.CountAsync();
            result.Data = await query.Select(a => new CaseByTypeReportModelData
            {
                Id = a.Id,
                MasterGestationalAgeId = a.TransConsultRoom != null ? a.TransConsultRoom.MasterGestationalAgeId : null,
                StartConsultDate = a.StartConsultDate.Value,
                Order = a.TransOrders.Any() ? a.TransOrders.Select(b => new CaseByTypeReportModelDataOrder
                {
                    Id = b.Id,
                    OrderItem = b.TransOrderItems.Any() ? b.TransOrderItems.Select(c => new CaseByTypeReportModelDataOrderItem
                    {
                        Id = c.Id,
                        MasterItemOrderId = c.MasterItemOrderId,
                    }).ToList() : new List<CaseByTypeReportModelDataOrderItem>(),
                }).ToList() : new List<CaseByTypeReportModelDataOrder>(),
            }).ToListAsync();

            return result;
        }


    }
}
