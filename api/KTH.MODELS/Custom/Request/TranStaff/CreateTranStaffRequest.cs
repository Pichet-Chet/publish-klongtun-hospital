using System;
using System.ComponentModel.DataAnnotations;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.TranStaff
{
	public class CreateTranStaffRequest
	{
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string SysRoleId { get; set; } = null!;

        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; } = null!;

        public string? NickName { get; set; }
        [Required(ErrorMessage = "IsActive is required")]
        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }
    }
}

