using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.Report
{
    public class MonthlyRefusalCaseReportResponse
    {
        public MonthlyRefusalCaseReportResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<MonthlyRefusalCaseReportResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MonthlyRefusalCaseReportResponseData
    {
        public DateOnly? ColA { get; set; } //วันที่

        public int ColB { get; set; } // โรคประจำตัว

        public int ColC { get; set; } // คดีความ

        public int ColD { get; set; } // ท้องนอกมดลูก

        public int ColE { get; set; } // ผ่าคลอด

        public int ColF { get; set; } // ครบคลอด

        public int ColG { get; set; } // ท้องโต

        public int ColH { get; set; } // รกเกาะต่ำ

        public int ColI { get; set; } // พร้อมมีบุตร

        public int ColJ { get; set; } // อื่นๆ

    }
}

