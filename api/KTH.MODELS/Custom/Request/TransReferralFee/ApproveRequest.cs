using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransReferralFee
{
    public class ApproveRequest
    {
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id {  get; set; }
        [Required(ErrorMessage = "SaleAmount is required")]
        public decimal SaleAmount { get; set; }
        [Required(ErrorMessage = "ReferralAmount is required")]
        public decimal ReferralAmount { get; set; }
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string ActionBy {  get; set; }
    }
}
