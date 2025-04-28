using System;
using System.ComponentModel.DataAnnotations;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.TransConsultComment
{
	public class UpdateTransConsultCommentRequest
	{
		public UpdateTransConsultCommentRequest()
		{
		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        [Required]
        public string? Description { get; set; }

        public bool IsPtChecked { get; set; }

        public bool IsActive { get; set; }

        public string? UpdatedBy { get; set; }
    }
}

