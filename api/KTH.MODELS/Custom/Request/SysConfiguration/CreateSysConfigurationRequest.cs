using System;
namespace KTH.MODELS.Custom.Request.SysConfiguration
{
	public class CreateSysConfigurationRequest
	{
		public CreateSysConfigurationRequest()
		{
            Group = string.Empty;

            Description = string.Empty;
        }

        public string Key { get; set; } = null!;

        public string Value { get; set; } = null!;

        public string? Group { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

    }
}

