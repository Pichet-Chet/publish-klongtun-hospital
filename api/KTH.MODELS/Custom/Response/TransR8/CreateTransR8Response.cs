using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransR8
{
    public class CreateTransR8Response
    {
        public MessageAlertResponse MessageAlert { get; set; }
        public CreateTransR8ResponseData Data { get; set; }
    }

    public class CreateTransR8ResponseData
    {
        public Guid Id { get; set; }
    }
}
