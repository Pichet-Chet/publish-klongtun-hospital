using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.MasterPaymentChannel
{
	public class UpdateMasterPaymentChannelRequest
	{
		public UpdateMasterPaymentChannelRequest()
		{
		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string NameTh { get; set; } = null!;

        public string NameEn { get; set; } = null!;

        public bool IsActive { get; set; }

        public string UpdatedBy { get; set; } = null!;
    }
}

