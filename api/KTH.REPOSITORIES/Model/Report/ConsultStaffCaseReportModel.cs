using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.REPOSITORIES.Model.Report
{
    public class ConsultStaffCaseReportModel
    {
        public int CountData { get; set; }
        public List<ConsultStaffCaseReportModelData> Data { get; set; }
    }

    public class ConsultStaffCaseReportModelData
    {
        public Guid Id { get; set; }
        public Guid? TransStaffId { get; set; }
        public string MasterStatusCode { get; set; }
        public Guid? MasterReasonNewAppointmentId { get; set; }
        public DateOnly ReceiveServiceDate { get; set; }
        public bool IsNewAppointment {  get; set; }
        public List<ConsultStaffCaseReportOrder> Order { get; set; }
    }

    public class ConsultStaffCaseReportOrder
    {
        public Guid Id { get; set; }
        public List<ConsultStaffCaseReportOrderItem> OrderItem { get; set; }
    }

    public class ConsultStaffCaseReportOrderItem
    {
        public Guid Id { get; set; }
        public Guid MasterOrderItemId { get; set; }
    }
}
