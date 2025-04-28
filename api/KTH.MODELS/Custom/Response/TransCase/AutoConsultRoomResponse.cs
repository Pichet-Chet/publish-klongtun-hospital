using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransCase
{
    public class AutoConsultRoomResponse
    {
        public MessageAlertResponse MessageAlert { get; set; }
        public AutoConsultRoomResponseData Data { get; set; }

    }

    public class AutoConsultRoomResponseData
    {
        public Guid? MasterConsultRoomId { get; set; }
        public string? MasterConsultRoomName { get; set; }
    }
}
