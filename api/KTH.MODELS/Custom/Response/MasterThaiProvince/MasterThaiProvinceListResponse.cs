using System;
using KTH.MODELS.Custom.Response.MasterThaiDistrict;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.MasterThaiProvince
{
    public class MasterThaiProvinceListResponse
    {
        public MasterThaiProvinceListResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<MasterThaiProvinceListResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MasterThaiProvinceListResponseData
    {
        public int Id { get; set; }

        public string NameTh { get; set; } = null!;

        public string NameEn { get; set; } = null!;

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}

