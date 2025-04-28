using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.TransSale
{
	public class LoginTransSaleRequest
	{
		public LoginTransSaleRequest()
		{
		}

		[Required]
		public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}

