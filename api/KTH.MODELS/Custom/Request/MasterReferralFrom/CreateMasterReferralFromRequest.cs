using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.CreateMasterReferral
{
    public class CreateMasterReferralFromRequest
    {
        public CreateMasterReferralFromRequest()
        {

        }

        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        [Required]
        public string CreatedBy { get; set; }
    }
}

