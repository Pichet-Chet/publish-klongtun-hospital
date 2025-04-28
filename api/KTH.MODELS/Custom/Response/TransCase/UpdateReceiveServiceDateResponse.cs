using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransCase
{
    public class UpdateReceiveServiceDateResponse
    {
        public UpdateReceiveServiceDateResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }
    public class UpdateReceiveServiceDateResponseData
    {
        public Guid Id { get; set; }
    }
}
