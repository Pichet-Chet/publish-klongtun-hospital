using System;
using KTH.MODELS.Helper;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.MasterReasonNewAppointment
{
	public class UpdateMasterReasonNewAppointmentRequest
	{
		public UpdateMasterReasonNewAppointmentRequest()
		{
		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        [Required]
        public string Group { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string UpdatedBy { get; set; } = null!;
    }
}

