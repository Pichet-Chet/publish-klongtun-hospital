using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.Report.Executive
{
	public class MonthlyPatientStatisticsReportRequest : FilterModel
    {
		public MonthlyPatientStatisticsReportRequest()
		{
		}

        [Required(ErrorMessage = "Year is required.")]
        public int Year { get; set; }
    }
}

