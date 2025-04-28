using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransOrder
{
    public class CreateTransOrderResponse
    {
        public CreateTransOrderResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class CreateTransOrderResponseData
    {
        public Guid Id { get; set; }
    }
}
