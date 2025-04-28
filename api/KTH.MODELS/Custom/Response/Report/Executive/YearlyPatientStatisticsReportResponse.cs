using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.Report.Executive
{
    public class YearlyPatientStatisticsReportResponse
    {
        public YearlyPatientStatisticsReportResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<YearlyPatientStatisticsReportResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class YearlyPatientStatisticsReportResponseData
    {
        public string? ColA { get; set; } // ปี

        public string? ColB { get; set; } // ม.ค.

        public string? ColC { get; set; } // ก.พ.

        public string? ColD { get; set; } // มึ.ค.

        public string? ColE { get; set; } // เม.ย.

        public string? ColF { get; set; } // พ.ค.

        public string? ColG { get; set; } // มิ.ย.

        public string? ColH { get; set; } // ก.ค.

        public string? ColI { get; set; } // ส.ค.

        public string? ColJ { get; set; } // ก.ย.

        public string? ColK { get; set; } // ต.ค.

        public string? ColL { get; set; } // พ.ย.

        public string? ColM { get; set; } // ธ.ค.


    }
}

