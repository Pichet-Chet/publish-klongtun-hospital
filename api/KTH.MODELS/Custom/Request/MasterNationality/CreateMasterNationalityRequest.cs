using System;
namespace KTH.MODELS.Custom.Request.MasterNationality
{
	public class CreateMasterNationalityRequest
	{
		public CreateMasterNationalityRequest()
		{
		}

        public string NameTh { get; set; } = null!;

        public string NameEn { get; set; } = null!;

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;
    }
}

