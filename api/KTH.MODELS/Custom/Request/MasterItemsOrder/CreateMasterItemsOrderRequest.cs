using System;
using System.ComponentModel.DataAnnotations;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.MasterItemsOrder
{
    public class CreateMasterItemsOrderRequest
    {
        public CreateMasterItemsOrderRequest()
        {

        }

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string MasterItemsOrderGroupId { get; set; } = null!;

        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }

        [Required]
        public float Price { get; set; }
    }
}

