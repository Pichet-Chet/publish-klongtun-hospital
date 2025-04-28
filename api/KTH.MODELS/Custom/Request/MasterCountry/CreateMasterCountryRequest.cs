using System;
namespace KTH.MODELS.Custom.Request.MasterCountry
{
	public class CreateMasterCountryRequest
    {
		public CreateMasterCountryRequest()
		{

        }

        public string Code { get; set; } = null!;

        public string NameTh { get; set; } = null!;

        public string NameEn { get; set; } = null!;

        public string TelephoneCode { get; set; } = null!;

        public string LanguageCode { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public string CreatedBy { get; set; } = null!;
    }
}

