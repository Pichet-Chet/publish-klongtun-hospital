using System;
using System.Text.Json.Serialization;
using KTH.MODELS.Custom.Response.SysPermission;

namespace KTH.MODELS.Custom.Response.SysRole
{
    public class SysRoleResponse
    {
        public SysRoleResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SysRoleResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class SysRoleResponseData
    {
        public SysRoleResponseData()
        {
            Name = string.Empty;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public bool IsActive { get; set; }

        public virtual ICollection<SysPermissionResponse> SysPermissions { get; set; } = new List<SysPermissionResponse>();

        //public virtual ICollection<TransStaff> TransStaffs { get; set; } = new List<TransStaff>();
    }
}

