using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransOrder
{
    public class GetTotalMoneyWithCaseResponse
    {
        public GetTotalMoneyWithCaseResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetTotalMoneyWithCaseResponseData
    {
        public decimal Total { get; set; }
        public decimal Overdue { get; set; }
        public decimal Paid { get; set; }
    }
}
