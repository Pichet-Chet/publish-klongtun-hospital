using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.Report.Executive
{
    public class WeeklyStoreCountAndCarriageFeeReportResponse
    {
        public WeeklyStoreCountAndCarriageFeeReportResponse()
        {
            MessageAlert = new MessageAlertResponse();
            Data = new List<WeeklyStoreCountAndCarriageFeeReportResponseData>();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<WeeklyStoreCountAndCarriageFeeReportResponseData> Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class WeeklyStoreCountAndCarriageFeeReportResponseData
    {
        public string? ColA { get; set; } // รายละเอียด

        public double ColB { get; set; } // จำนวนคนไข้

        public double ColC { get; set; } // จำนวนค่านำพา
    }
}

