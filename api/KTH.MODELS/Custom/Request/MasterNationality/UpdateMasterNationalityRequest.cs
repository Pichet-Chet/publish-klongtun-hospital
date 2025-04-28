using System;
using System.ComponentModel.DataAnnotations;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.MasterNationality
{
	public class UpdateMasterNationalityRequest
	{
		public UpdateMasterNationalityRequest()
		{
		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        public string NameTh { get; set; } = null!;

        public string NameEn { get; set; } = null!;

        public bool IsActive { get; set; }

        public string UpdatedBy { get; set; } = null!;

    }
}

