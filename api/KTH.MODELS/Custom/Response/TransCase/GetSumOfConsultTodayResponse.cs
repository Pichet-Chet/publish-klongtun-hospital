using System;
namespace KTH.MODELS.Custom.Response.TransCase
{
    public class GetSumOfConsultTodayResponse
    {
        public GetSumOfConsultTodayResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetSumOfConsultTodayResponseData
	{
        public GetSumOfConsultTodayResponseData()
        {
            WaitingConsultPercent = string.Empty;
            ConsultedPercent = string.Empty;
            ClientPercent = string.Empty;
            AcceptPercent = string.Empty;
        }

        public int WaitingConsult { get; set; } // นัดหมายวันนี้

        public string WaitingConsultPercent { get; set; } // นัดหมายวันนี้ (เปอร์เซ็น)

        public int Consulted { get; set; } // คนไข้ Walk-in

        public string ConsultedPercent { get; set; }// คนไข้ Walk-in (เปอร์เซ็น)

        public int Client { get; set; } // คนไข้ลงทะเบียน

        public string ClientPercent { get; set; } // คนไข้ลงทะเบียน (เปอร์เซ็น)

        public int Accept { get; set; } // คนไข้ทั้งหมดของวันนี้

        public string AcceptPercent { get; set; } // คนไข้ทั้งหมดของวันนี้ (เปอร์เซ็น)
    }
}

