using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.Report.Executive
{
	public class WeeklyStoreCountAndCarriageFeeReportRequest : FilterModel
    {
		public WeeklyStoreCountAndCarriageFeeReportRequest()
		{
		}

        [Required(ErrorMessage = "Year is required.")]
        public int Year { get; set; }
    }
}

