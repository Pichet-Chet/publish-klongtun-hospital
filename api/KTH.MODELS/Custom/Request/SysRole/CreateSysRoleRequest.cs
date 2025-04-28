using System;
namespace KTH.MODELS.Custom.Request.SysRole
{
	public class CreateSysRoleRequest
	{
		public CreateSysRoleRequest()
		{
		}

        public string Name { get; set; } = null!;

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }
    }
}

