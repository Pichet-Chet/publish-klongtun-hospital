using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransLab
{
    public class CreateTransLabResponse
    {
        public MessageAlertResponse MessageAlert { get; set; }
        public CreateTransLabResponseData Data { get; set; }
    }

    public class CreateTransLabResponseData
    {
        public Guid Id { get; set; }
    }
}
