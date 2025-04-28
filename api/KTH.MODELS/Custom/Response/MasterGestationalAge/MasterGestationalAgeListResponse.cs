using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.MasterGestationalAge
{
    public class MasterGestationalAgeListResponse
    {
        public MasterGestationalAgeListResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<MasterGestationalAgeListResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }


    public class MasterGestationalAgeListResponseData
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }

}

