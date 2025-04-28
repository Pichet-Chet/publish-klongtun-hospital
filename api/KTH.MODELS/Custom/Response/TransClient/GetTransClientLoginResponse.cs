using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TranStaff
{
    public class GetTransClientLoginResponse
    {
        public GetTransClientLoginResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetTransClientLoginResponseData
    {
        public string AccessToken { get; set; }
    }
}
