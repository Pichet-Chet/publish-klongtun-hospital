using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.Report.Executive
{
    public class WeeklyStoreCarriageFeeReportResponse
    {
        public WeeklyStoreCarriageFeeReportResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<WeeklyStoreCarriageFeeReportResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class WeeklyStoreCarriageFeeReportResponseData
    {
        public string? ColA { get; set; } // รายละเอียด

        public double ColB { get; set; } // จำนวนคนไข้

        public double ColC { get; set; } // จำนวนค่านำพา


    }
}

