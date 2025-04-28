using System;
namespace KTH.MODELS.Custom.Request.TransConsultRoom
{
    public class CreateTransConsultRoomRequest
    {
        public CreateTransConsultRoomRequest()
        {

        }

        public Guid? Id { get; set; }

        public string TransCaseId { get; set; } = null!;

        public string MasterGestationalAgeId { get; set; } = null!;

        public string MasterReferralFromId { get; set; } = null!;

        public string MasterChannelInformationId { get; set; } = null!;

        public bool DrugAllergy { get; set; }

        public string? DrugAllergyRemark { get; set; }

        public bool CongenitalDisease { get; set; }

        public string? CongenitalDiseaseRemark { get; set; }

        public bool CaesareanSection { get; set; }

        public bool Relatives { get; set; }

        public bool Patient { get; set; }

        public string? CreatedBy { get; set; }

        public bool Withdraw { get; set; }

        public bool IsForeigner { get; set; }

        public string TransStaffId { get; set; } = null!;

    }
}

