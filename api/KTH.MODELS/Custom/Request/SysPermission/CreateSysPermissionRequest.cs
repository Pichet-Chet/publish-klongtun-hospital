using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.SysPermission
{
	public class CreateSysPermissionRequest
	{

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string SysRoleId { get; set; } = null!;

        public string Page { get; set; } = null!;

        public string Action { get; set; } = null!;

        public string CreatedBy { get; set; } = null!;
    }
}

