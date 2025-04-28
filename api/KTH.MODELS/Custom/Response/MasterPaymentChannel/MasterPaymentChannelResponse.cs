using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.MasterPaymentChannel
{
    public class MasterPaymentChannelResponse
    {
        public MasterPaymentChannelResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MasterPaymentChannelResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MasterPaymentChannelResponseData
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = null!;

        public string? NameTh { get; set; }

        public string? NameEn { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}

