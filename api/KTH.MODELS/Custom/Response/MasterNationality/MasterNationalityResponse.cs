using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.MasterNationality
{
    public class MasterNationalityResponse
    {
        public MasterNationalityResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MasterNationalityResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MasterNationalityResponseData
	{
        public Guid Id { get; set; }

        public string NameTh { get; set; } = null!;

        public string NameEn { get; set; } = null!;

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }
    }
}

