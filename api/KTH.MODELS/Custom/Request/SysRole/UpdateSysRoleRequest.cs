using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.SysRole
{
	public class UpdateSysRoleRequest
	{
		public UpdateSysRoleRequest()
		{
		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        public string Name { get; set; } = null!;

        public bool IsActive { get; set; }

        public string? UpdatedBy { get; set; }
    }
}

