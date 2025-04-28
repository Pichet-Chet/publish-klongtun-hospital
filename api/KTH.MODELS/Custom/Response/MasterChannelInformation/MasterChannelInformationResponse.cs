using System;
using KTH.MODELS.Custom.Response.MasterPhysician;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.MasterChannelInformation
{
    public class MasterChannelInformationResponse
    {
        public MasterChannelInformationResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MasterChannelInformationResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MasterChannelInformationResponseData
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

