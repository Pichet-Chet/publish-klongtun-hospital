using System;
namespace KTH.MODELS.Custom.Response.TransCase
{
    public class GetSumOfAppointmentTodayResponse
    {
        public GetSumOfAppointmentTodayResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetSumOfAppointmentTodayResponseData
    {
		public GetSumOfAppointmentTodayResponseData()
		{
            AppointmentPercent = string.Empty;
            WalkinPercent = string.Empty;
            RegisterPercent = string.Empty;
            AllPercent = string.Empty;
        }

		public decimal Appointment { get; set; } // นัดหมายวันนี้

		public string AppointmentPercent { get; set; } // นัดหมายวันนี้ (เปอร์เซ็น)

        public decimal Walkin { get; set; } // คนไข้ Walk-in

        public string WalkinPercent { get; set; }// คนไข้ Walk-in (เปอร์เซ็น)

        public decimal Register { get; set; } // คนไข้ลงทะเบียน

        public string RegisterPercent { get; set; } // คนไข้ลงทะเบียน (เปอร์เซ็น)

        public decimal All { get; set; } // คนไข้ทั้งหมดของวันนี้

        public string AllPercent { get; set; } // คนไข้ทั้งหมดของวันนี้ (เปอร์เซ็น)
    }
}

