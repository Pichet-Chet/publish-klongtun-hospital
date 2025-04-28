using System;
using KTH.MODELS.Custom.Response.MasterGestationalAge;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.MasterHospital
{
    public class MasterHospitalListResponse
    {
        public MasterHospitalListResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<MasterHospitalListResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MasterHospitalListResponseData
	{
        public Guid Id { get; set; }

        public string NameTh { get; set; } = null!;

        public string? NameEn { get; set; }

        public int? Code { get; set; }

        public string? Department { get; set; }

        public string? Type { get; set; }

        public bool IsActive { get; set; }

        public string? MasterThaiProvincesNameTh { get; set; }

        public int? MasterThaiProvincesNameId { get; set; }
    }
}

