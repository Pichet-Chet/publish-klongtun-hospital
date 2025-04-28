using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.SysConfiguration
{
	public class UpdateSysConfigurationRequest
	{
        public UpdateSysConfigurationRequest()
        {
            Group = string.Empty;

            Description = string.Empty;
        }

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        public string Key { get; set; } = null!;

        public string Value { get; set; } = null!;

        public string? Group { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public string? UpdatedBy { get; set; }
    }
}

