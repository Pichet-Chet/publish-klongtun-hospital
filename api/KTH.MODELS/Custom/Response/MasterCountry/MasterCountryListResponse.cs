using System;
using KTH.MODELS.Custom.Response.MasterConsultRoom;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.MasterCountry
{
    public class MasterCountryListResponse
    {
        public MasterCountryListResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<MasterCountryListResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MasterCountryListResponseData
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = null!;

        public string NameTh { get; set; } = null!;

        public string NameEn { get; set; } = null!;

        public string TelephoneCode { get; set; } = null!;

        public string? LanguageCode { get; set; }

        public bool IsActive { get; set; }

        public string? Flag { get; set; }
    }
}

