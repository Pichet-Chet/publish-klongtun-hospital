 using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.Report
{
    public class MonthlyAppointmentReportResponse
    {
        public MonthlyAppointmentReportResponse()
        {
            MessageAlert = new MessageAlertResponse();
            Data = new List<MonthlyAppointmentReportResponseData>();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<MonthlyAppointmentReportResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MonthlyAppointmentReportResponseData
    {
        public DateOnly ColA { get; set; } // วันที่

        public int ColB { get; set; } // ทำนัดหมาย

        public int ColC { get; set; } // นัดกลับมา

        public int ColD { get; set; } // ค่าใช้จ่ายไม่พอ

        public int ColE { get; set; } // ขอปรึกษาญาติ

        public int ColF { get; set; } // ให้ผู้ปกครองมาเซ็น

        public int ColG { get; set; } // ติดธุระ

        public int ColH { get; set; } // ติดค้างคืน

        public int ColI { get; set; } // U/S ซ้ำ

        public int ColJ { get; set; } // มาสอบถามราคา

        public int ColK { get; set; } // นัดพบแพทย์

        public int ColL { get; set; } // เดี๋ยวมา

        public int ColM { get; set; } // รอแฟนมาเซ็น

        public int ColN { get; set; } // อื่นๆ
    }


}

