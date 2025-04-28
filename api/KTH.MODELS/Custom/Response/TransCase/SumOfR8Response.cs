using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransCase
{
    public class SumOfR8Response
    {
        public MessageAlertResponse MessageAlert { get; set; }
        public SumOfR8ResponseData Data { get; set; }
    }

    public class SumOfR8ResponseData
    {
        public int WaitForR8 { get; set; }
        public int Success { get; set; }
        public int AddLab { get; set; }
    }
}
