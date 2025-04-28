using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.Finance
{
    public class AccountRefundFinanceResponse
    {
        public List<AccountRefundFinanceDetail> Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class AccountRefundFinanceDetail
    {
        public Guid TransPaymentId { get; set; }
        public string TramsPaymentNo { get; set; }
        public Guid TransCaseId { get; set; }
        public string TransCaseNo { get; set; }
        public string CreateDate { get; set; }
        public string TransPaymentType { get; set; }
        public string System { get; set; }
        public Guid TransClientId { get; set; }
        public string TransClientName { get; set; }
        public decimal Cash { get; set; }
        public decimal Qr { get; set; }
        public decimal Credit { get; set; }
        public decimal Reserve { get; set; }
        public decimal Refund { get; set; }
        public string IsReceipt { get; set; }
        public string WithDraw { get; set; }
        public string AccountRefundDate { get; set; }
        public string AccountRefundBy { get; set; }
    }
}
