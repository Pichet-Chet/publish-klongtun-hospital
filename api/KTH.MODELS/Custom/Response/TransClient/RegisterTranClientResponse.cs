using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.TransClient
{
    public class RegisterTranClientResponse
    {
        public RegisterTranClientResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public RegisterTranClientResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class RegisterTranClientResponseData
    {
        public string TransClientId { get; set; } = null!;

        public string TransCaseId { get; set; } = null!;

    }
}

