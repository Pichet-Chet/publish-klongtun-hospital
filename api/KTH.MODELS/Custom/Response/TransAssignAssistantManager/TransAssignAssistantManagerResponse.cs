using KTH.MODELS.Custom.Response.MasterRightTreatment;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.TransAssignAssistantManager
{
    public class TransAssignAssistantManagerResponse
    {
        public TransAssignAssistantManagerResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TransAssignAssistantManagerResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class TransAssignAssistantManagerResponseData
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = null!;

        public string? Name { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }

        public string Password { get; set; } = null!;
    }
}

