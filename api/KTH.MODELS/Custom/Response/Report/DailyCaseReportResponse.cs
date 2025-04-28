using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.Report
{
    public class DailyCaseReportResponse
    {
        public DailyCaseReportResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<DailyCaseReportResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class DailyCaseReportResponseData
    {
        public int ColA { get; set; } // No.

        public string? ColB { get; set; } // Case No.

        public string? ColC { get; set; } // ชื่อ-นามสกุล

        public bool ColD { get; set; } // U/S

        public bool ColE { get; set; } // U/S เก็บเงิน

        public bool ColF { get; set; } // U/S ฟรี

        public bool ColG { get; set; } // PT

        public bool ColH { get; set; } // PT เก็บเงิน

        public bool ColI { get; set; } // P/T ฟรี

        public bool ColJ { get; set; } // Follow

        public string? ColK { get; set; } // นัด

        public string? ColL { get; set; } // Week

        public string? ColM { get; set; } // หมายเหตุ

    }
}

