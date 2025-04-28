using System;
using KTH.MODELS.Custom.Response.MasterThaiDistrict;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.MasterThaiProvince
{
    public class MasterThaiProvinceResponse
    {
        public MasterThaiProvinceResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MasterThaiProvinceResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MasterThaiProvinceResponseData
	{
        public int Id { get; set; }

        public string NameTh { get; set; } = null!;

        public string NameEn { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}

