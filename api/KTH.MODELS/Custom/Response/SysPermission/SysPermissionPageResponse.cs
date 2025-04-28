using System;
using KTH.MODELS.Custom.Response.SysConfiguration;
using System.Text.Json.Serialization;
using KTH.MODELS.Custom.Response.SysRole;

namespace KTH.MODELS.Custom.Response.SysPermission
{
    public class SysPermissionPageResponse
    {
        public SysPermissionPageResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public PermissionData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class PermissionData
    {
        public string roleName { get; set; } = null!;

        public List<string> Permission { get; set; } = null!;

    }
}

