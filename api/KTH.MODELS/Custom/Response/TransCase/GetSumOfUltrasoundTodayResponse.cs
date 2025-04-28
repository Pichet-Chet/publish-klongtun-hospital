using System;
namespace KTH.MODELS.Custom.Response.TransCase
{
    public class GetSumOfUltrasoundTodayResponse
    {
        public GetSumOfUltrasoundTodayResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetSumOfUltrasoundTodayResponseData
    {
        public GetSumOfUltrasoundTodayResponseData()
        {
            WaitingUsPercent = string.Empty;
            UsPercent = string.Empty;
        }

        public int WaitingUs { get; set; } // นัดหมายวันนี้

        public string WaitingUsPercent { get; set; } // นัดหมายวันนี้ (เปอร์เซ็น)

        public int Us { get; set; } // คนไข้ Walk-in

        public string UsPercent { get; set; }// คนไข้ Walk-in (เปอร์เซ็น)
    }
}

