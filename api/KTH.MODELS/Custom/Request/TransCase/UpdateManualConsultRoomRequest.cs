using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.TransCase
{
	public class UpdateManualConsultRoomRequest
	{
		public UpdateManualConsultRoomRequest()
		{
            Id = string.Empty;
            MasterConsultRoomId = string.Empty;
        }

        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "MasterConsultRoomId is required")]
        public string MasterConsultRoomId { get; set; }

        [Required(ErrorMessage = "Update by is required")]
        public string UpdatedBy { get; set; }

    }
}

