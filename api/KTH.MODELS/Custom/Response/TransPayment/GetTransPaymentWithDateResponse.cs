using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransPayment
{
    public class GetTransPaymentWithDateResponse
    {
        public List<GetTransPaymentWithDateResponseData> Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }
    public class GetTransPaymentWithDateResponseData
    {
        public Guid Id { get; set; }

        public string TransactionNo { get; set; } = null!;

        public DateTime TransactionDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string PaymentChanel { get; set; } = null!;

        public string? PaymentChanelCard { get; set; }

        public string? Remark { get; set; }

        public Guid MasterStatusId { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsReceipt { get; set; }

        public Guid TransOrderId { get; set; }

        public List<GetTransPaymentWithTransactionNoResponseDataItem> TransPaymentItem { get; set; }
    }

    public class GetTransPaymentWithDateResponseDataItem
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
