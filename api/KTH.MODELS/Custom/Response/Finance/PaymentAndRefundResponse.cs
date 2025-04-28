using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.Finance
{
    public class PaymentAndRefundResponse
    {
        public List<PaymentAndRefundResponseData> Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class PaymentAndRefundResponseData
    {
        public Guid TransactionId { get; set; }
        public string TransactionNo {  get; set; }
        public Guid CaseId { get; set; }
        public string CaseNo { get; set; }
        public string Status {  get; set; }
        public string POS { get; set; }
        public string ActionDate {  get; set; }
        public string PaymentType { get; set; }
        public string System {  get; set; }
        public string ClientName {  get; set; }
        public decimal? Cash { get; set; }
        public decimal? Qr {  get; set; }
        public decimal? Credit { get; set;}
        public decimal? Reserve { get; set; }
        public decimal? RefundReserve { get; set; }
        public string IsReceipt {  get; set; }
        public string IsWithdraw { get; set; }
        public string Remark {  get; set; }
        public string ActionName {  get; set; }
        public bool IsAction {  get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
