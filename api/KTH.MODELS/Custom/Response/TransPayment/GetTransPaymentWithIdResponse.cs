using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransPayment
{
    public class GetTransPaymentWithIdResponse
    {
        public GetTransPaymentWithIdResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }
    public class GetTransPaymentWithIdResponseData
    {
        public Guid Id { get; set; }

        public string TransactionNo { get; set; } = null!;

        public DateTime TransactionDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string PaymentChanel { get; set; } = null!;

        public string? PaymentChanelCard { get; set; }

        public string? Remark { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsReceipt { get; set; }

        public Guid? TransOrderId { get; set; }

        public string MasterStatusCode { get; set; } = null!;

        public List<GetTransPaymentWithIdResponseDataItem> TransPaymentItem { get; set; }
    }

    public class GetTransPaymentWithIdResponseDataItem
    {
        public Guid Id { get; set; }

        public Guid TransTransactionPaymentHeaderId { get; set; }

        public Guid TransOrderItemId { get; set; }

        public decimal Amount { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
