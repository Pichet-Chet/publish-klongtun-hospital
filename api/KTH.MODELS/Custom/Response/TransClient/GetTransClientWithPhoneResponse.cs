using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransClient
{
    public class GetTransClientWithPhoneResponse 
    {
        public GetTransClientWithPhoneResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetTransClientWithPhoneResponseData
    {
        public bool isDuplicate { get; set; }

        public string FullName { get; set; }

        public DateOnly DateOfBirth { get; set; }
    }
}
