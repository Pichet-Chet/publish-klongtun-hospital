using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.Report.Executive
{
	public class MonthlyStoreCountAndCarriageFeeReportRequest : FilterModel
    {
		public MonthlyStoreCountAndCarriageFeeReportRequest()
		{
		}

        [Required(ErrorMessage = "Year is required.")]
        public int YearStart { get; set; }

        [Required(ErrorMessage = "Year is required.")]
        public int YearEnd { get; set; }
    }
}

