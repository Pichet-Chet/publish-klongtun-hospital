using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.MasterCountry
{
	public class UpdateMasterCountryRequest
    {
		public UpdateMasterCountryRequest()
		{
		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        public string NameTh { get; set; } = null!;

        public string NameEn { get; set; } = null!;

        public string TelephoneCode { get; set; } = null!;

        public string LanguageCode { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public string UpdatedBy { get; set; } = null!;
    }
}

