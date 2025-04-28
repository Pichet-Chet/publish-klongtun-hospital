using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.MasterThaiDistrict
{

    public class MasterThaiDistrictListResponse
    {
        public MasterThaiDistrictListResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<MasterThaiDistrictListResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MasterThaiDistrictListResponseData
	{
        public int Id { get; set; }

        public string NameTh { get; set; } = null!;

        public string NameEn { get; set; } = null!;

        public int MasterThaiProvincesId { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}

