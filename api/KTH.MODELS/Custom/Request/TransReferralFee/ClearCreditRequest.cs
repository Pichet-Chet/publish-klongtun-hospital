using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransReferralFee
{
    public class ClearCreditRequest
    {
        [ValidGuid(ErrorMessage = "Invalid Guid Format")]
        public string Id { get; set; }
        [ValidGuid(ErrorMessage = "Invalid Guid Format")]
        public string ActionBy { get; set; }
    }
}
