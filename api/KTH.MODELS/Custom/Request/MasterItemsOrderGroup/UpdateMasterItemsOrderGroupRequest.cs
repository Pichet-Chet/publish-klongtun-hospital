using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.MasterItemsOrderGroup
{
    public class UpdateMasterItemsOrderGroupRequest
    {
        public UpdateMasterItemsOrderGroupRequest()
        {

        }

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public string? UpdatedBy { get; set; }
    }
}

