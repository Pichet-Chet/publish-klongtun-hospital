using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.MasterChannelInformation
{
    public class CreateMasterChannelInformationRequest
    {
        public CreateMasterChannelInformationRequest()
        {
        }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string CreatedBy { get; set; } = null!;
    }
}

