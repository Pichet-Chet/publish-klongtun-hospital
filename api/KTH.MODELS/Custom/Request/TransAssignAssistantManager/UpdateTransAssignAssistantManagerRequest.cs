using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.TransAssignAssistantManager
{
	public class UpdateTransAssignAssistantManagerRequest
	{
		public UpdateTransAssignAssistantManagerRequest()
		{
		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        public bool IsActive { get; set; }

        public string? UpdatedBy { get; set; }
    }
}

