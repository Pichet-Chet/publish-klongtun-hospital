using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.Report
{
    public class YearlyCaseReportResponse
    {
        public YearlyCaseReportResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<YearlyCaseReportResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class YearlyCaseReportResponseData
    {
        public int ColA { get; set; } //วันที่

        public int ColB { get; set; } // เดือนมกราคม

        public int ColC { get; set; } // เดือนกุมภาพันธ์

        public int ColD { get; set; } // เดือนมีนาคม

        public int ColE { get; set; } // เดือนเมษายน

        public int ColF { get; set; } // เดือนพฤษภาคม

        public int ColG { get; set; } // เดือนมิถุนายน

        public int ColH { get; set; } // เดือนกรกฎาคม

        public int ColI { get; set; } // เดือนสิงหาคม

        public int ColJ { get; set; } // เดือนกันยายน

        public int ColK { get; set; } // เดือนตุลาคม

        public int ColL { get; set; } // เดือนพฤศจิกายน

        public int ColM { get; set; } // เดือนธันวาคม

    }
}

