using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransReferralFee
{
    public class WaitForApproveRequest
    {
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id {  get; set; }
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string ApproveId { get; set; }
        [Required(ErrorMessage = "ApproveLevel is required")]
        public int ApproveLevel {  get; set; }
    }
}
