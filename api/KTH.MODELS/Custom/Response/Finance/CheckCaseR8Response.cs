using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.Finance
{
    public class CheckCaseR8Response
    {
        public MessageAlertResponse MessageAlert { get; set; }
        public CheckCaseR8ResponseData Data { get; set; }
    }

    public class CheckCaseR8ResponseData
    {
        public bool IsSend { get; set; }
    }
}
