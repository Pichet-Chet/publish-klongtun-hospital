using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.MasterConsultRoom
{
	public class UpdateConsultRoomOwnerRequest
	{
		public UpdateConsultRoomOwnerRequest()
		{
		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; }

        public bool IsActive { get; set; }

		public string? Owner { get; set; }
    }
}

