using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.SysPermission
{
	public class UpdateSysPermissionRequest
	{
		public UpdateSysPermissionRequest()
		{
		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string SysRoleId { get; set; } = null!;

        public string Page { get; set; } = null!;

        public string Action { get; set; } = null!;

        public bool IsActive { get; set; }

        public string UpdatedBy { get; set; } = null!;
    }
}

