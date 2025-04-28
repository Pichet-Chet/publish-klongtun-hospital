using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.TransClient
{
    public class UpdatePhoneTransClientResponse
    {
        public UpdatePhoneTransClientResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public UpdatePhoneTransClientResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class UpdatePhoneTransClientResponseData
    {
        public string PhoneUpdated { get; set; } = null!;
    }
}

