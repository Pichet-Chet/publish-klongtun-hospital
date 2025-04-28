using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransPayment
{
    public class GetAccountRefundFilterRequest : FilterModel
    {
        public DateTime? StartActionDate { get; set; }
        public DateTime? EndActionDate { get;  set; }

        public DateTime? StartActionRefund {  get; set; }
        public DateTime? EndActionRefund { get;set; }
    }
}
