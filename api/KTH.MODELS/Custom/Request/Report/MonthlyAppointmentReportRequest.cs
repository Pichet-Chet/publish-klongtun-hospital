using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.Report
{
    public class MonthlyAppointmentReportRequest : FilterModel
    {
        public MonthlyAppointmentReportRequest()
        {
        }
        [Required]
        public string Year { get; set; } = null!;

        [Required]
        public string Month { get; set; } = null!;
    }
}

