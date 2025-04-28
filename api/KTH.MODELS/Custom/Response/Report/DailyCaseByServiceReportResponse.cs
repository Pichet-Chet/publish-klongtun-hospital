using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.Report
{
    public class DailyCaseByServiceReportResponse
    {
        public DailyCaseByServiceReportResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<DailyCaseByServiceReportResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class DailyCaseByServiceReportResponseData
    {
        public DateOnly? ColA { get; set; } //วันที่

        public int ColB { get; set; } // ผู้มาติดต่อ

        public int ColC { get; set; } // ลูกค้าใหม่

        public int ColD { get; set; } // Follow

        public int ColE { get; set; } // นัดกลับมา

        public int ColF { get; set; } // U/S ทั้งหมด

        public int ColG { get; set; } // U/S เก็บเงิน

        public int ColH { get; set; } // U/S ซ้ำ/ฟรี

        public int ColI { get; set; } // P/T ทั้งหมด

        public int ColJ { get; set; } // P/T เก็บเงิน

        public int ColK { get; set; } // P/T ซ้ำ, ฟรี

    }
}

