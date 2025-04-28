using System;
using System.ComponentModel.DataAnnotations;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.MasterReasonNotTreatment
{
	public class UpdateMasterReasonNotTreatmentRequest
	{
		public UpdateMasterReasonNotTreatmentRequest()
		{
		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string UpdatedBy { get; set; } = null!;
    }
}

