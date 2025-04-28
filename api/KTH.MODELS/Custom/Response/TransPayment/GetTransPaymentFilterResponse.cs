using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransPayment
{
    public class GetTransPaymentFilterResponse
    {
        public List<GetTransPaymentFilterResponseData> Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetTransPaymentFilterResponseData
    {
        public Guid Id { get; set; }

        public string TransactionNo { get; set; } = null!;

        public DateTime TransactionDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string? TypePayment { get; set; }

        public string? Remark { get; set; }

        public string? CreatedBy { get; set; }

        public bool IsReceipt { get; set; }

        public string TransCaseNo { get; set; }

        public string POS { get; set; }

        public Guid TransCaseId { get; set; }

        public string PaymentChannel {  get; set; }

        public List<GetTransPaymentFilterResponseDataItem> TransPaymentItem { get; set; }
    }

    public class GetTransPaymentFilterResponseDataItem
    {
        public decimal Amount { get; set; }
    }
}
