using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransReferralFee
{
    public class CreateTransSummaryRequest
    {
        [Required(ErrorMessage = "EndDate is required")]
        public DateOnly EndDate { get; set; }

        [ValidGuid(ErrorMessage = "Invalid Guid Format")]
        public string ActionBy { get; set; }
    }
}
