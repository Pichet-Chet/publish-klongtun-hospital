using System;
namespace KTH.MODELS.ThirdParty.SMTP
{
	public class SmtpSendModel
	{
		public SmtpSendModel()
		{
		}


		public string To { get; set; } = null!;

		public string Subject { get; set; } = null!;

        public string UserAccount { get; set; } = null!;

        public string UserPassword { get; set; } = null!;

    }
}

