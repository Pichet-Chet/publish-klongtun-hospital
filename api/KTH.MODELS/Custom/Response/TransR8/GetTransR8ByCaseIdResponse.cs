using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransR8
{
    public class GetTransR8ByCaseIdResponse
    {
        public MessageAlertResponse MessageAlert { get; set; }
        public GetTransR8ByCaseIdResponseData Data { get; set; }
    }

    public class GetTransR8ByCaseIdResponseData
    {
        public Guid Id { get; set; }
        public Guid CaseId { get; set; }
        public bool TestResult { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
    }
}
