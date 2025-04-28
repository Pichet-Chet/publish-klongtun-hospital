using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.Report
{
    public class ForeignClientReportRequest : FilterModel
    {
        public ForeignClientReportRequest()
        {

        }

        [Required]
        public string Year { get; set; } = null!;

        [Required]
        public string Month { get; set; } = null!;
    }
}

