using KTH.MODELS.Custom.Response.MasterRightTreatment;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.SummaryPeriodIncome
{
    public class DocumentClosePeriodGenerateResponse
    {
        public DocumentClosePeriodGenerateResponse()
        {
            MessageAlert = new MessageAlertResponse();
            Data = new DocumentClosePeriodGenerateResponseData();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DocumentClosePeriodGenerateResponseData Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class DocumentClosePeriodGenerateResponseData
    {
        public DocumentClosePeriodGenerateResponseData()
        {
            StreamResult = new MemoryStream();
        }

        public MemoryStream StreamResult { get; set; }
    }
}

