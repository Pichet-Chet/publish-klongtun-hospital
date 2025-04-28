using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransCase
{
    public class GetSumOfConsultTodayManagerResponse
    {
        public GetSumOfConsultTodayManagerResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetSumOfConsultTodayManagerResponseData
    {
        public List<ClassDetail> WaitingConsultDetail { get; set; }
        public int WaitingConsult { get; set; } // นัดหมายวันนี้

        public string WaitingConsultPercent { get; set; } // นัดหมายวันนี้ (เปอร์เซ็น)

        public List<ClassDetail> ConsultedDetail { get; set; }

        public int Consulted { get; set; } // คนไข้ Walk-in

        public string ConsultedPercent { get; set; }// คนไข้ Walk-in (เปอร์เซ็น)

        public List<ClassDetail> ClientDetail { get; set; }

        public int Client { get; set; } // คนไข้ลงทะเบียน

        public string ClientPercent { get; set; } // คนไข้ลงทะเบียน (เปอร์เซ็น)

        public List<ClassDetail> AcceptDetail { get; set; }

        public int Accept { get; set; } // คนไข้ทั้งหมดของวันนี้

        public string AcceptPercent { get; set; } // คนไข้ทั้งหมดของวันนี้ (เปอร์เซ็น)
    }

    public class ClassDetail
    {
        public string RoomName { get; set; }
        public int Summary { get; set; }
    }
}
