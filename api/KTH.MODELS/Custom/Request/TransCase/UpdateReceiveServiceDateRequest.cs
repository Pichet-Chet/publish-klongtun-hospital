using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransCase
{
    public class UpdateReceiveServiceDateRequest
    {
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string CaseId { get; set; }
        [Required(ErrorMessage = "ReceiveServiceDate is required")]
        public DateOnly ReceiveServiceDate { get; set; }
    }
}
