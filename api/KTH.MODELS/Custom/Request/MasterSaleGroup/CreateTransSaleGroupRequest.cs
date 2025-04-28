using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.MasterSaleGroup
{
    public class CreateMasterSaleGroupRequest
    {
        public CreateMasterSaleGroupRequest()
        {

        }

        [Required]
        public string Name { get; set; } = null!;

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;
    }
}

