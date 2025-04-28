using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KTH.MODELS.Constants.ConstantsMassage;

namespace KTH.MODELS.Custom.Request.TransClient
{
    public class GetTransClientLoginRequest
    {
        [Required(ErrorMessage = "Telephone number is required")]
        [StringLength(16,ErrorMessage = "Telephone number be long than 16 characters")]
        public string TelephoneNumber {  get; set; }
        [Required(ErrorMessage = "DateOfBirth is required")]
        public DateOnly DateOfBirth { get; set; }
        [Required(ErrorMessage = "Telephone code is required")]
        public string TelephoneCode { get; set; }

    }
}
