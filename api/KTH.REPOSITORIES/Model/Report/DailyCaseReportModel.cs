using System;
namespace KTH.REPOSITORIES.Model.Report
{
    public class DailyCaseReportModel
    {
        public int CountData { get; set; }
        public List<DailyCaseReportModelData> Data { get; set; }
    }

    public class DailyCaseReportModelData
    {
        public Guid Id { get; set; }
        public string? CaseNo { get; set; }
        public string? FullName { get; set; }
        public bool? Us { get; set; }
        public bool? FreeUs { get; set; }
        public bool? Pt { get; set; }
        public bool? FreePt { get; set; }
        public bool? IsFollow { get; set; }
        public string? DateAppointment { get; set; }
        public string? Week { get; set; }
        public string? Remark { get; set; }
    }
}

