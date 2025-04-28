using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.Report
{
    public class ConsultStaffCaseReportRequest : FilterModel
    {
        public ConsultStaffCaseReportRequest()
        {
        }

        [Required(ErrorMessage = "StartDate is required")]
        public DateOnly StartDate { get; set; }

        [Required(ErrorMessage = "EndDate is required")]
        public DateOnly EndDate { get; set; }
    }
}

