using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransReferralFee
{
    public class GetReferralHistoryResponse
    {
        public List<GetReferralHistoryResponseData> Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetReferralHistoryResponseData
    {
        public string DateAction { get; set; }
        public string ChangeReferralFee {  get; set; }
        public string ChangeCredit {  get; set; }
        public string Remark {  get; set; }
    }
}
