using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.TransCaseCancel
{
	public class CreateTransCaseCancelRequest
	{

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string TransCaseId { get; set; } = null!;

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string MasterReasonNotTreatmentId { get; set; } = null!;

        public string Remark { get; set; } = null!;

        public string CreatedBy { get; set; } = null!;
    }
}

