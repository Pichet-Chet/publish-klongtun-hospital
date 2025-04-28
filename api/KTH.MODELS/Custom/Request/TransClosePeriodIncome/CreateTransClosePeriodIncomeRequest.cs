using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.TransClosePeriodIncome
{
	public class CreateTransClosePeriodIncomeRequest
	{
		public CreateTransClosePeriodIncomeRequest()
		{
		}

        [Required]
        public string MoneyBucket { get; set; } = null!;

        [Required]
        public string RoleName { get; set; } = null!;

        [Required]
        public string ActionBy { get; set; } = null!;

    }
}

