using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.TransCase
{
	public class UpdatePtCaseServiceRequest
	{
		public UpdatePtCaseServiceRequest()
		{
		}

		[ValidGuid(ErrorMessage = "Invalid Guid format")]
		public string CaseId { get; set; } = null!;

		public string? PtRemark { get; set; }

		public string PtByStaffId { get; set; } = null!;
    }
}

