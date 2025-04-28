using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransResultPt
{
    public class GetTransResultPtByTransCaseIdResponse
    {
        public MessageAlertResponse MessageAlert { get; set; }
        public GetTransResultPtByTransCaseIdResponseData Data { get; set; }

    }

    public class GetTransResultPtByTransCaseIdResponseData
    {
        public Guid Id { get; set; }
        public Guid TransCaseId { get; set; }
        public bool ResultPt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }
}
