using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransReferralFee
{
    public class ApproveSummaryRequest
    {
        [ValidGuid(ErrorMessage = "Invalid Guid Format")]
        public string Id {  get; set; }
        [ValidGuid(ErrorMessage = "Invalid Guid Format")]
        public string ActionBy { get; set; }

        [Required(ErrorMessage = "Action is required")]
        public string Action { get; set; }

    }
}
