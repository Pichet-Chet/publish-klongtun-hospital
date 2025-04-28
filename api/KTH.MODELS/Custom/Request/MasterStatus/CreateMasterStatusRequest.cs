using System;
namespace KTH.MODELS.Custom.Request.MasterStatus
{
	public class CreateMasterStatusRequest
	{
		public CreateMasterStatusRequest()
		{
		}

        public string Group { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

    }
}

