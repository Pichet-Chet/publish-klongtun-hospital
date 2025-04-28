using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.TranStaff
{
    public class UpdatePasswordRequest
    {
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string ConfirmPassword { get; set; } = null!;
    }
}

