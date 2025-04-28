using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransReferralFee
{
    public class WaitForApproveResponse
    {
        public WaitForApproveResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class WaitForApproveResponseData
    {
        public Guid Id { get; set; }
    }
}
