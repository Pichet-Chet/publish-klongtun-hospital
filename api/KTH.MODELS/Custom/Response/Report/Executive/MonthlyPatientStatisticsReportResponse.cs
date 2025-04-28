using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.Report.Executive
{
	public class MonthlyPatientStatisticsReportResponse
	{
        public MonthlyPatientStatisticsReportResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<MonthlyPatientStatisticsReportResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MonthlyPatientStatisticsReportResponseData
    {
        public MonthlyPatientStatisticsReportResponseData()
        {
            ColBPercent = string.Empty;
            ColCPercent = string.Empty;
            ColDPercent = string.Empty;
            ColEPercent = string.Empty;
        }

        public string? ColA { get; set; } // รายละเอียด

        public double ColB { get; set; } // คนไข้เข้า
        public string? ColBPercent { get; set; } // คนไข้เข้า (เปอร์เซ็น)

        public double ColC { get; set; } // ตกลงทำ
        public string? ColCPercent { get; set; } // ตกลงทำ (เปอร์เซ็น)

        public double ColD { get; set; } // นัดกลับมา
        public string? ColDPercent { get; set; } // นัดกลับมา (เปอร์เซ็น)

        public double ColE { get; set; } // นัด
        public string? ColEPercent { get; set; } // นัด (เปอร์เซ็น)
    }
}

