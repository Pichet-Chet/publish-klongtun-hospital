using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TranStaff
{
    public class GetTransStaffLoginResponse
    {
        public GetTransStaffLoginResponseData Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetTransStaffLoginResponseData
    {
        public GetTransStaffLoginResponseData()
        {
            permissionLoginData = new PermissionLoginData();
        }

        public Guid Id { get; set; }

        public Guid SysRoleId { get; set; }

        public string SysRoleName { get; set; }

        public string Username { get; set; } = null!;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Password { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string? NickName { get; set; }

        public string? AccessToken { get; set; }

        public DateTime? AccessTokenExpire { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public PermissionLoginData permissionLoginData { get; set; }
    }


    public class PermissionLoginData
    {
        public List<string> Permission { get; set; } = null!;
    }


}
