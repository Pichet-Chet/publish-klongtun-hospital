using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransOrder
{
    public class UpdateTransOrderApproveRequest
    {
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string TransOrderId { get;set; }

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string TransCaseId {  get; set; }

        [Required(ErrorMessage = "MasterStatusCode is required")]
        public string TransCaseMasterStatusCode { get;set; }

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string StaffApproveId { get; set; }

        public string remarkSpecialDiscountApprove {  get; set; }
    }
}
