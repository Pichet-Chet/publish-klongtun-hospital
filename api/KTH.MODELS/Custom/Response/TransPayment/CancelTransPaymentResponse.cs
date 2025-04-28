using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransPayment
{
    public class CancelTransPaymentResponse
    {
        public CancelTransPaymentResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class CancelTransPaymentResponseData
    {
        public Guid Id { get; set; }
    }
}
