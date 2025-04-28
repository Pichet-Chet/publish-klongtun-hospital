using System;
using System.ComponentModel.DataAnnotations;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.Report.Executive
{
	public class WeeklyStoreCarriageFeeReportRequest : FilterModel
    {
		public WeeklyStoreCarriageFeeReportRequest()
		{
		}

        [Required(ErrorMessage = "Year is required.")]
        public int Year { get; set; }

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string MasterSaleGroupId { get; set; } = null!;

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string MasterReferralFromId { get; set; } = null!;
    }
}

