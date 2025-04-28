using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.TransActionHistory
{
	public class CreateTransActionHistoryRequest
	{
		public CreateTransActionHistoryRequest()
		{
		}

        [Required]
        public string TransCaseId { get; set; } = null!;

        [Required]
        public string TransCaseStatus { get; set; } = null!;

        [Required]
        public string ActionName { get; set; } = null!;

        [Required]
        public string ActionBy { get; set; } = null!;
    }
}

