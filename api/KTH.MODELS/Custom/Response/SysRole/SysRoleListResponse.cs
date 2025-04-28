using System;
using System.Text.Json.Serialization;
using KTH.MODELS.Custom.Response.SysPermission;

namespace KTH.MODELS.Custom.Response.SysRole
{
    public class SysRoleListResponse
    {
        public SysRoleListResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<SysRoleListResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class SysRoleListResponseData
    {
        public SysRoleListResponseData()
        {
            Name = string.Empty;

            SysPermissions = new List<SysPermissionResponse>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public List<SysPermissionResponse>? SysPermissions { get; set; }

        //public virtual ICollection<TransStaff> TransStaffs { get; set; } = new List<TransStaff>();
    }
}

