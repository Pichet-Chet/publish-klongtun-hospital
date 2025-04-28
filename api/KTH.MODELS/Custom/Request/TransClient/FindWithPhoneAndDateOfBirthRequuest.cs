using System;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.TransClient
{
	public class FindWithPhoneAndDateOfBirthRequuest
	{
		public FindWithPhoneAndDateOfBirthRequuest()
		{
		}

        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "DateOfBirth is required")]
        public DateOnly? DateOfBirth { get; set; }

        
    }
}

