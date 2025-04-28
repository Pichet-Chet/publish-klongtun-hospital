using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransCase
{
    public class UnfollowCaseResponse
    {
        public UnfollowCaseResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }
    public class UnfollowCaseResponseData
    {
        public Guid Id { get; set; }
    }
}
