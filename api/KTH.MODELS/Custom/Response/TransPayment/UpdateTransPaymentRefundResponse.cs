using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransPayment
{
    public class UpdateTransPaymentRefundResponse
    {
        public UpdateTransPaymentRefundResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class UpdateTransPaymentRefundResponseData
    {
        public Guid Id { get; set; }
    }
}
