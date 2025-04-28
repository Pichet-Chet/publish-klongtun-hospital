using System;
namespace KTH.MODELS.Custom.Request.MasterPaymentChannel
{
	public class CreateMasterPaymentChannelRequest
	{
		public CreateMasterPaymentChannelRequest()
		{
		}

        public string Code { get; set; } = null!;

        public string NameTh { get; set; } = null!;

        public string NameEn { get; set; } = null!;

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;
    }
}

