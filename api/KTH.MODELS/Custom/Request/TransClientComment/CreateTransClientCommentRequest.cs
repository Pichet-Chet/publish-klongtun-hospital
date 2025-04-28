using System;
using System.ComponentModel.DataAnnotations;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.TransClientComment
{
	public class CreateTransClientCommentRequest
	{
		public CreateTransClientCommentRequest()
		{

		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string TransClientId { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;
    }
}

