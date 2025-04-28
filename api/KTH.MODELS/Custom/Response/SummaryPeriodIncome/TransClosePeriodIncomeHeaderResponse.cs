using System;
using KTH.MODELS.Custom.Response.MasterNationality;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.SummaryPeriodIncome
{
    public class TransClosePeriodIncomeHeaderResponse
    {
        public TransClosePeriodIncomeHeaderResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TransClosePeriodIncomeHeaderResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class TransClosePeriodIncomeHeaderResponseData
    {
        public TransClosePeriodIncomeHeaderResponseData()
        {
            TransClosePeriodIncomeDetails = new List<TransClosePeriodIncomeDetailData>();
        }

        public Guid Id { get; set; }

        public DateTime PeriodStartDatetime { get; set; }

        public DateTime PeriodEndDatetime { get; set; }

        public string RoleName { get; set; } = null!;

        public string MoneyBucket { get; set; } = null!;

        public DateTime? SummaryDatetime { get; set; }

        public string? ActionName { get; set; }

        public virtual List<TransClosePeriodIncomeDetailData> TransClosePeriodIncomeDetails { get; set; }
    }

    public class TransClosePeriodIncomeDetailData
    {
        public Guid Id { get; set; }

        public Guid? TransPaymentHeaderId { get; set; }

        public decimal? Cash { get; set; }

        public decimal? QrCode { get; set; }

        public string? CreditCard { get; set; }

        public decimal? Summary { get; set; }
    }
}

