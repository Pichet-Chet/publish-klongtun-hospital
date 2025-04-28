using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.Finance
{
    public class GetAccountRefund1663FilterResponse
    {
        public List<GetAccountRefund1663FilterResponseData> Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetAccountRefund1663FilterResponseData
    {
        public Guid Id { get; set; }
        public string MonthYear { get; set; }
        public decimal Price { get; set; }
        public decimal Pay { get; set; }
        public decimal x1663 { get; set; }
        public string Cashier { get; set; }
        public string ReceiveDate { get; set; }
        public bool IsRefund { get; set; }
    }
}
