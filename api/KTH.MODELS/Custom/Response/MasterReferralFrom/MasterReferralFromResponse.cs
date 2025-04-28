using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.MasterReferralFrom
{
    public class MasterReferralFromResponse
    {
        public MasterReferralFromResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MasterReferralFromResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MasterReferralFromResponseData
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }
    }
}

