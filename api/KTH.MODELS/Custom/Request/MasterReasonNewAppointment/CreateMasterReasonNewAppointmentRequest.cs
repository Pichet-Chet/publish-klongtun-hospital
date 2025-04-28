using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.MasterReasonNewAppointment
{
	public class CreateMasterReasonNewAppointmentRequest
	{

        [Required]
        public string Group { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string CreatedBy { get; set; } = null!;
    }
}

