using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.TransClient
{
	public class CreateTransClientPartnerPageRequest
	{
		public CreateTransClientPartnerPageRequest()
		{
		}

        // Part 1

        public string FullName { get; set; } = null!;

        [Required(ErrorMessage = "DateOfBirth is required")]
        public DateOnly? DateOfBirth { get; set; }

        public string TelephoneNumber { get; set; } = null!;

        public string TranSaleRefCode { get; set; } = null!;


        // Part 2

        public int GestationalAge { get; set; }

        public string SaleRecord { get; set; }

        // Part 3

        [Required(ErrorMessage = "ReceiveServiceDate is required")]
        public DateOnly? ReceiveServiceDate { get; set; }
    }
}

