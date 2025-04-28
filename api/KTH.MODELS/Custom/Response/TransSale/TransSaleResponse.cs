using System;
using KTH.MODELS.Custom.Response.MasterPhysician;
using System.Text.Json.Serialization;
using KTH.MODELS.Custom.Response.MasterSaleGroup;

namespace KTH.MODELS.Custom.Response.TransSale
{
    public class TransSaleResponse
    {
        public TransSaleResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TransSaleResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }


    public class TransSaleResponseData
	{
        public Guid Id { get; set; }

        public string Username { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string? NickName { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }

        public virtual MasterSaleGroupResponse? MasterSaleGroup { get; set; }

        public string AccessToken { get; set; }

        public Guid  SysRoleId { get; set; }

        public Guid MasterSaleGroupId { get; set; }

        public DateTime? AccessTokenExpire { get; set; }

        public string RefCode { get; set; } = null!;
    }
}

