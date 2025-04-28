using System;
namespace KTH.MODELS.Custom.Request.MasterGestationalAge
{
	public class CreateMasterGestationalAgeRequest
	{
		public CreateMasterGestationalAgeRequest()
		{
		}

        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

		public string? CreatedBy { get; set; }
    }
}

