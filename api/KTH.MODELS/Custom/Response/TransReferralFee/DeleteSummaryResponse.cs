using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransReferralFee
{
    public class DeleteSummaryResponse
    {
        public DeleteSummaryResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class DeleteSummaryResponseData
    {
        public Guid Id { get; set; }
    }
}
