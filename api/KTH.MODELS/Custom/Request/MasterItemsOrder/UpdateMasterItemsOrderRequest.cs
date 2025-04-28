using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.MasterItemsOrder
{
	public class UpdateMasterItemsOrderRequest
	{
		public UpdateMasterItemsOrderRequest()
		{

		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string MasterItemsOrderGroupId { get; set; } = null!;

        public string? Name { get; set; }

        public string? Description { get; set; }

        public float Price { get; set; }

        public bool IsActive { get; set; }

        public string? UpdatedBy { get; set; }
    }
}

