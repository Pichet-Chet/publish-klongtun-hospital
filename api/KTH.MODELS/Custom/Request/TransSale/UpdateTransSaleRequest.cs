using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.TransSale
{
	public class UpdateTransSaleRequest
	{
		public UpdateTransSaleRequest()
		{
		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string MasterSaleGroupId { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string? NickName { get; set; }

        public bool IsActive { get; set; }

        public string UpdatedBy { get; set; } = null!;
    }
}

