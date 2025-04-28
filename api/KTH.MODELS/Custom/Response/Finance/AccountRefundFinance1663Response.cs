using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.Finance
{
    public class AccountRefundFinance1663Response
    {
        public List<AccountRefundFinance1663Case> Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class AccountRefundFinance1663Case
    {
        public string CreateDate { get; set; }
        public string CaseNo { get; set; }
        public Guid CaseId { get; set; }
        public string ClientName { get; set; }
        public string Courier { get; set; }
        public List<ItemDetail> ItemDetailList { get; set; }
    }

    public class ItemDetail
    {
        public string OrderNo {  get; set; }
        public Guid OrderId { get; set; }
        public string ItemName { get; set; }
        public Guid ItemId { get; set; }
        public decimal Price { get; set; }
        public decimal Pay { get; set; }
        public decimal x1663 { get; set; }
    }

}
