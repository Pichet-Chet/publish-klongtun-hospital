using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.REPOSITORIES.Model.Report
{
    public class CaseByTypeReportModel
    {
        public int CountData { get; set; }
        public List<CaseByTypeReportModelData> Data { get; set; }
    }

    public class CaseByTypeReportModelData
    {
        public Guid Id { get; set; }
        public Guid? MasterGestationalAgeId { get; set; }
        public DateTime StartConsultDate { get; set; }
        public List<CaseByTypeReportModelDataOrder> Order { get; set; }
    }

    public class CaseByTypeReportModelDataOrder
    {
        public Guid Id { get; set; }
        public List<CaseByTypeReportModelDataOrderItem> OrderItem { get; set; }
    }

    public class CaseByTypeReportModelDataOrderItem
    {
        public Guid Id { get; set; }
        public Guid MasterItemOrderId { get; set; }
    }
}
