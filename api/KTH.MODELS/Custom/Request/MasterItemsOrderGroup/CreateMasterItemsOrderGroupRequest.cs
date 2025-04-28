using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.MasterItemsOrderGroup
{
    public class CreateMasterItemsOrderGroupRequest
    {
        public CreateMasterItemsOrderGroupRequest()
        {

        }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        public string? CreatedBy { get; set; }
    }
}

