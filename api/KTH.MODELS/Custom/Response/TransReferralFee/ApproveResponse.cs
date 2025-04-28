using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransReferralFee
{
    public class ApproveResponse
    {
        public MessageAlertResponse MessageAlert {  get; set; }
        public ApproveResponseData Data { get; set; }

    }

    public class ApproveResponseData
    {
        public Guid Id { get; set; }
    }
}
