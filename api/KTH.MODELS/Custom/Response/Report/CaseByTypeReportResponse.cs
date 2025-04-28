using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.Report
{
    public class CaseByTypeReportResponse
    {
        public CaseByTypeReportResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<CaseByTypeReportResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    } 

    public class CaseByTypeReportResponseData
    {
        public DateOnly ColA { get; set; } //วันที่

        public int ColB { get; set; } // M-ทำเลย

        public int ColC { get; set; } // M-ทานยา

        public int ColD { get; set; } // M-พิเศษทานยา

        public int ColE { get; set; } // M-พิเศษทำเลย

        public int ColF { get; set; } // รวม M

        public int ColG { get; set; } // P-ธรรมดา

        public int ColH { get; set; } // P-พิเศษ

        public int ColI { get; set; } // รวม P

        public int ColJ { get; set; } // H-ธรรมดา

        public int ColK { get; set; } // H-พิเศษ

        public int ColL { get; set; } // รวม H

        public int ColM { get; set; } // รวมเคส 

    }
}

