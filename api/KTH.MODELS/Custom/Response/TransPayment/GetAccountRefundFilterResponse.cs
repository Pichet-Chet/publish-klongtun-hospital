using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransPayment
{
    public class GetAccountRefundFilterResponse
    {
        public GetAccountRefundFilterResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetAccountRefundFilterResponseData
    {
        public List<GetAccountRefundFilterData> ListData { get; set; }
    }

    public class GetAccountRefundFilterData
    {
        public Guid TransPaymentId { get; set; }
        public string TransPaymentNo { get; set; }
        public string TransCaseNo { get; set; }
        public Guid TransCaseId { get; set; }
        public DateTime ActionDate { get; set; }
        public string TransPaymentType { get; set; }
        public string TransPaymentBucket { get; set; }
        public string FullName { get; set; }
        public decimal? Cash { get; set; }
        public decimal? Qr { get; set; }
        public decimal? Credit { get; set; }
        public decimal? Reserve { get; set; }
        public decimal? Refund { get; set; }
        public bool IsReceipt { get; set; }
        public bool WithDraw { get; set; }
        public Guid? AccountRefundId { get; set; }
        public string AccountRefundName { get; set; }
    }
}
