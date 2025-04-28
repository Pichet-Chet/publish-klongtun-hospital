using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransPayment
{
    public class CreateTransPaymentReceiveRequest
    {
        public decimal TotalAmount { get; set; }
        public string? Remark { get; set; }

        public string? CreatedBy { get; set; }
        [Required(ErrorMessage = "IsReceipt is required")]
        public bool IsReceipt { get; set; }
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string TransOrderId { get; set; }

        public List<CreateTransPaymentItemRequest> Item { get; set; }

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string TransClientId { get; set; }

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string TransCaseId { get; set; }
        [Required(ErrorMessage = "PaymentCash is required")]
        public decimal PaymentCash { get; set; }
        [Required(ErrorMessage = "PaymentQrCode is required")]
        public decimal PaymentQrCode { get; set; }
        [Required(ErrorMessage = "PaymentCredit is required")]
        public decimal PaymentCredit { get; set; }

        public string? PaymentCreditCard { get; set; }
    }

    public class CreateTransPaymentItemRequest
    {
        public string TransOrderItemId { get; set; }
    }
}
