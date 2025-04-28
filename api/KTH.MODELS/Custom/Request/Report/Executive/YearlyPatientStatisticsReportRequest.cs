using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.Report.Executive
{
	public class YearlyPatientStatisticsReportRequest : FilterModel
    {
		public YearlyPatientStatisticsReportRequest()
		{
		}

        [Required(ErrorMessage = "Year is required.")]
        public int Year { get; set; }


    }
}

