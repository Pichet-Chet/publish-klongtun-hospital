using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransLr
{
    public class AddTransLrResponse
    {
        public MessageAlertResponse MessageAlert { get; set; }
        public AddTransLrResponseData Data { get; set; }
    }

    public class AddTransLrResponseData
    {
        public Guid Id { get; set; }
    }
}
