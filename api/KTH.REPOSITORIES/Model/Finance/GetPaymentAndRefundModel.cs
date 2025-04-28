using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.REPOSITORIES.Model.Finance
{
    public class GetPaymentAndRefundModel
    {
        public int CountData { get; set; }
        public List<GetPaymentAndRefundModelData> Data { get; set; }
    }

    public class GetPaymentAndRefundModelData
    {
        public Guid TransactionId { get; set; }
        public string TransactionNo { get; set; }
        public Guid CaseId { get; set; }
        public string CaseNo { get; set; }
        public string CaseMasterStatusCode { get; set; }
        public string MasterStatusCode { get; set; }
        public string TypePayment { get; set; }
        public short MoneyBucket { get; set; }
        public string ClientFullName { get; set; }
        public decimal Cash { get; set; }
        public decimal Qr { get; set; }
        public decimal Credit { get; set; }
        public decimal TotalAmount { get; set; }
        public GetPaymentAndRefundModelDataOrder Order { get; set; }
        public bool IsReceipt { get; set; }
        public bool WithDraw { get; set; }
        public string Remark { get; set; }
        public DateTime UpdateDate { get; set; }
        public string UpdateBy { get; set; }

    }

    public class GetPaymentAndRefundModelDataOrder
    {
        public Guid Id { get; set; }
        public List<GetPaymentAndRefundModelDataOrderItem>? OrderItem { get; set; }
    }

    public class GetPaymentAndRefundModelDataOrderItem
    {
        public Guid Id { get; set; }
        public decimal Reserve { get; set; }
    }

}
