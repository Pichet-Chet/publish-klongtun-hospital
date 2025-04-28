using System;
using KTH.MODELS.Custom.Response.MasterPhysician;
using System.Text.Json.Serialization;
using KTH.MODELS.Custom.Response.MasterSaleGroup;

namespace KTH.MODELS.Custom.Response.TransSale
{
    public class TransSaleListResponse
    {
        public TransSaleListResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<TransSaleListResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }


    public class TransSaleListResponseData
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

        public TasnsSaleMasterSaleGroupData? MasterSaleGroup { get; set; }

        public string RefCode { get; set; } = null!;
    }

    public class TasnsSaleMasterSaleGroupData
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = null!;

        public string? Name { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }
    }
}

