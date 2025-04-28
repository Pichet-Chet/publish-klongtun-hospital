using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.MasterReferralFrom
{
    public class UpdateMasterReferralFromRequest
    {
        public UpdateMasterReferralFromRequest()
        {
        }

        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        public string? UpdatedBy { get; set; }
    }
}

