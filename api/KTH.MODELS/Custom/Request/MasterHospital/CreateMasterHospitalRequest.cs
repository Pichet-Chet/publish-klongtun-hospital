using System;
namespace KTH.MODELS.Custom.Request.MasterHospital
{
	public class CreateMasterHospitalRequest
	{
		public CreateMasterHospitalRequest()
		{

		}

        public string NameTh { get; set; } = null!;

        public string? NameEn { get; set; }

        public int? Code { get; set; }

        public string? Department { get; set; }

        public string? Type { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

    }
}

