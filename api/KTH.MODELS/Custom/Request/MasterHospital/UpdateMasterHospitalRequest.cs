using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.MasterHospital
{
	public class UpdateMasterHospitalRequest
	{
		public UpdateMasterHospitalRequest()
		{
		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        public string NameTh { get; set; } = null!;

        public string? NameEn { get; set; }

        public int? Code { get; set; }

        public string? Department { get; set; }

        public string? Type { get; set; }

        public bool IsActive { get; set; }

        public string? UpdatedBy { get; set; }
    }
}

