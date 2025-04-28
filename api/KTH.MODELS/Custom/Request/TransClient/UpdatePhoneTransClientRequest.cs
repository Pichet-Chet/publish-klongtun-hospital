using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.TransClient
{
	public class UpdatePhoneTransClientRequest
	{
		public UpdatePhoneTransClientRequest()
		{
		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        public string OldPhone { get; set; } = null!;

        public string NewPhone { get; set; } = null!;
    }
}

