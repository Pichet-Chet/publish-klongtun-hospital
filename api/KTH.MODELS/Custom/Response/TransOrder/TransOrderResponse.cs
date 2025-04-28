using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.TransOrder
{
    public class TransOrderResponse
    {
        public TransOrderResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TransOrderResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }


    public class TransOrderResponseData
    {
        public Guid Id { get; set; }

        public Guid TransCaseId { get; set; }

        public string? RemarkOrder { get; set; }

        public string? RemarkSpecialDiscountRequest { get; set; }

        public string? RemarkSpecialDiscountApprove { get; set; }

        public string? MasterStatusCode { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string? CreatedBySysRoleCode { get; set; }
    }
}

