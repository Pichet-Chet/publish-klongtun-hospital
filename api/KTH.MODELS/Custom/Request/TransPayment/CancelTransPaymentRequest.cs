using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransPayment
{
    public class CancelTransPaymentRequest
    {
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; }

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string UpdatedBy { get; set; }
    }
}
