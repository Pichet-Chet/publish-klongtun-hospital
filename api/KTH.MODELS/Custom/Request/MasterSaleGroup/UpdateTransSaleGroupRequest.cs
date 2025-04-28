using System;
using System.ComponentModel.DataAnnotations;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.MasterSaleGroup
{
	public class UpdateMasterSaleGroupRequest
	{
		public UpdateMasterSaleGroupRequest()
		{
		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;

        public bool IsActive { get; set; }

        public string UpdatedBy { get; set; } = null!;
    }
}

