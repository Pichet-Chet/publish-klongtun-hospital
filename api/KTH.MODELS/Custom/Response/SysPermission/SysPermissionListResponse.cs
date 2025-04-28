using System;
using KTH.MODELS.Custom.Response.SysConfiguration;
using System.Text.Json.Serialization;
using KTH.MODELS.Custom.Response.SysRole;

namespace KTH.MODELS.Custom.Response.SysPermission
{
    public class SysPermissionListResponse
    {
        public SysPermissionListResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<SysPermissionListResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class SysPermissionListResponseData
    {
        public SysPermissionListResponseData()
        {
            SysRole = new SysRoleResponse();

            Page = string.Empty;

            Action = string.Empty;
        }

        public Guid Id { get; set; }

        public Guid SysRoleId { get; set; }

        public string Page { get; set; }

        public string Action { get; set; }

        public bool IsActive { get; set; }

        public virtual SysRoleResponse SysRole { get; set; }

    }
}

