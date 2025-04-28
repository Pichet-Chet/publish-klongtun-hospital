using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransReferralFee
{
    public class ChangeRegerralFeeRequest
    {
        [ValidGuid(ErrorMessage = "Invalid Guid Format")]
        public string ReferralId {  get; set; }
        [Required(ErrorMessage = "ReferralFee is required")]
        public decimal ReferralFee {  get; set; }
        [Required(ErrorMessage = "Credit is required")]
        public decimal Credit {  get; set; }
        public string? Remark {  get; set; }
        [Required(ErrorMessage = "ActionBy is required")]
        public string ActionBy {  get; set; }
    }
}
