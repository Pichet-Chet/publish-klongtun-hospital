using System;
using KTH.MODELS.Custom.Response.TransActionHistory;
using System.Text.Json.Serialization;
using KTH.MODELS.Custom.Response.TransClient;

namespace KTH.MODELS.Custom.Response.TransClosePeriodIncome
{
    public class TransClosePeriodIncomeResponse
    {
        public TransClosePeriodIncomeResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TransClosePeriodIncomeResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class TransClosePeriodIncomeResponseData
    {
        public Guid Id { get; set; }

        public string PeriodDate { get; set; } = null!;

        public string RoleName { get; set; } = null!;

        public string MoneyBucket { get; set; } = null!;

        public string? SummaryDatetime { get; set; }

        public string? ActionName { get; set; }

        public string ClosePeriodNo { get; set; } = null!;
    }
}

