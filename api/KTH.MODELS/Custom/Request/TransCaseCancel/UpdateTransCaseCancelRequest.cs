using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.TransCaseCancel
{
	public class UpdateTransCaseCancelRequest
	{
		public UpdateTransCaseCancelRequest()
		{
		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string TransCaseId { get; set; } = null!;

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string MasterReasonNotTreatmentId { get; set; } = null!;

        public string? Remark { get; set; }

        public string UpdatedBy { get; set; } = null!;
    }
}

