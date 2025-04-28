using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransCase
{
    public class CheckCaseNewOrOldResponse
    {
        public MessageAlertResponse MessageAlert { get; set; }
        public CheckCaseNewOrOldResponseData Data { get; set; }
    }

    public class CheckCaseNewOrOldResponseData
    {
        public bool IsNew { get; set; }
    }
}
