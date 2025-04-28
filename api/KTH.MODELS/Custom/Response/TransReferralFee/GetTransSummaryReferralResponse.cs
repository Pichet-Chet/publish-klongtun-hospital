using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransReferralFee
{
    public class GetTransSummaryReferralResponse
    {
        public List<GetTransSummaryReferralResponseData> Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetTransSummaryReferralResponseData
    {
        public Guid Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal TotalCase { get; set; }
        public decimal TotalReferral { get; set; }
        public decimal TotalCredit { get; set; }
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
        public bool IsPrint { get; set; }
        public string HeaderNo { get; set; }
    }
}
