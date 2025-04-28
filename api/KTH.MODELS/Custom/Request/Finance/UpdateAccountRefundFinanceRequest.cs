using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.Finance
{
    public class UpdateAccountRefundFinanceRequest
    {
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string TransPaymentId {  get; set; }
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string ActionBy {  get; set; }
    }
}
