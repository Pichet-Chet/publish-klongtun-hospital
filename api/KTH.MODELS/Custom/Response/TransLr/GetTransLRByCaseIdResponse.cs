using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransLr
{
    public class GetTransLRByCaseIdResponse
    {
        public MessageAlertResponse MessageAlert { get; set; }
        public GetTransLRByCaseIdResponseData Data { get; set; }
    }

    public class GetTransLRByCaseIdResponseData
    {
        public Guid Id { get; set; }
        public Guid CaseId { get; set; }
        public string HNNo { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }
}
