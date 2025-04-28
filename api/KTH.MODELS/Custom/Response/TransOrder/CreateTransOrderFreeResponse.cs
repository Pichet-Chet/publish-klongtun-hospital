using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransOrder
{
    public class CreateTransOrderFreeResponse
    {
        public MessageAlertResponse MessageAlert { get; set; }
        public CreateTransOrderFreeResponseData Data { get; set; }
    }

    public class CreateTransOrderFreeResponseData
    {
        public Guid Id { get; set; }
    }
}
