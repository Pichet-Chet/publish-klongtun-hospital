using System;
using KTH.MODELS.Custom.Response.MasterThaiProvince;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.MasterThaiSubDistrict
{
    public class MasterThaiSubDistrictResponse
    {
        public MasterThaiSubDistrictResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MasterThaiSubDistrictResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MasterThaiSubDistrictResponseData
	{
        public int Id { get; set; }

        public int? PostCode { get; set; }

        public string NameTh { get; set; } = null!;

        public string NameEn { get; set; } = null!;

        public int? MasterThaiDistrictsId { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }
    }
}

