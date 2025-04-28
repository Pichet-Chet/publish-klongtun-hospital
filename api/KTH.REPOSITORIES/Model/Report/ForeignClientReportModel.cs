using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.REPOSITORIES.Model.Report
{
    public class ForeignClientReportModel
    {
        public List<ForeignClientReportModelData> Data { get; set; }
        public int CountData { get; set; }
    }

    public class ForeignClientReportModelData
    {
        public Guid Id { get; set; }
        public DateOnly ReceiveServiceDate { get; set; }
        public string ClientName { get; set; }
        public string CaseNo { get; set; }
        public string TeamSaleName { get; set; }
        public string From { get; set; }
        public string Mo { get; set; }
        public List<ForeignClientReportModelDataOrder> Order { get; set; }
    }

    public class ForeignClientReportModelDataOrder
    {
        public Guid Id { get; set; }
        public List<ForeignClientReportModelDataOrderItem> OrderItem { get; set; }

    }

    public class ForeignClientReportModelDataOrderItem
    {
        public Guid Id { get; set; }
        public Guid MasterItemOrderId { get; set; }
    }
}
