using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransCase
{
    public class ManualConsultRoomResponse
    {
        public MessageAlertResponse MessageAlert { get; set; }
        public ManualConsultRoomResponseData Data { get; set; }
    }

    public class ManualConsultRoomResponseData
    {
        public Guid? MasterConsultRoomId { get; set; }
        public string? MasterConsultRoomName { get; set; }
    }
}
