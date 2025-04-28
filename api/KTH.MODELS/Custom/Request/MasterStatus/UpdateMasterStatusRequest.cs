using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.MasterStatus
{
	public class UpdateMasterStatusRequest
	{
		public UpdateMasterStatusRequest()
		{
		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        public string Group { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public string UpdatedBy { get; set; } = null!;
    }
}

