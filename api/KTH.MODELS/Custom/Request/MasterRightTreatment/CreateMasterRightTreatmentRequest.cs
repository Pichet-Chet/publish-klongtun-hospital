using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.MasterRightTreatment
{
	public class CreateMasterRightTreatmentRequest
	{
		public CreateMasterRightTreatmentRequest()
		{
		}

		[Required]
        public string Name { get; set; } = null!;

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string CreatedBy { get; set; } = null!;

    }
}

