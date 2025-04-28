using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.TransConsultRoom
{
    public class TransConsultRoomResponse
    {
        public TransConsultRoomResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TransConsultRoomResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class TransConsultRoomResponseData
    {
        public Guid Id { get; set; }

        public Guid TransCaseId { get; set; }

        public Guid MasterGestationalAgeId { get; set; }

        public Guid MasterReferralFromId { get; set; }

        public Guid MasterChannelInformationId { get; set; }

        public bool DrugAllergy { get; set; }

        public string? DrugAllergyRemark { get; set; }

        public bool CongenitalDisease { get; set; }

        public string? CongenitalDiseaseRemark { get; set; }

        public bool CaesareanSection { get; set; }

        public bool Relatives { get; set; }

        public bool Patient { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool Withdraw { get; set; }

        public bool IsForeigner { get; set; }

    }
}

