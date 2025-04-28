using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransResultPt
{
    public class CreateTransResultResponse
    {
        public MessageAlertResponse MessageAlert { get; set; }
        public CreateTransResultResponseData Data { get; set; }

    }

    public class CreateTransResultResponseData
    {
        public Guid Id { get; set; }
    }
}
