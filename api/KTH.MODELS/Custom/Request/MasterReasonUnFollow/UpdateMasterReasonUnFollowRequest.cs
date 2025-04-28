using System;
using System.ComponentModel.DataAnnotations;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.MasterReasonUnFollow
{
    public class UpdateMasterReasonUnFollowRequest
    {
        public UpdateMasterReasonUnFollowRequest()
        {
        }

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string UpdatedBy { get; set; } = null!;
    }
}

