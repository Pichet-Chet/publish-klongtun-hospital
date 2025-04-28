using System;
namespace KTH.MODELS.Custom.Request.TransClient
{
	public class FindWithPhoneRequuest
	{
		public FindWithPhoneRequuest()
		{
			Code = string.Empty;

			Phone = string.Empty;
		}

		public string Code { get; set;}

		public string Phone { get; set; }
	}
}

