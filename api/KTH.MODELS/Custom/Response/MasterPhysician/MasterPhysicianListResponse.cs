using System;
using KTH.MODELS.Custom.Response.MasterNationality;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.MasterPhysician
{
    public class MasterPhysicianListResponse
    {
        public MasterPhysicianListResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<MasterPhysicianListResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MasterPhysicianListResponseData
    {
        public Guid Id { get; set; }

        public string Prefix { get; set; } = null!;

        public string Name { get; set; } = null!;

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }
    }
}

