using KTH.MODELS.Custom.Response.MasterRightTreatment;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.SummaryPeriodIncome
{
    public class DocumentCoverSheetGenerateResponse
    {
        public DocumentCoverSheetGenerateResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DocumentCoverSheetGenerateResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class DocumentCoverSheetGenerateResponseData
    {
        public byte[] StreamResult { get; set; }
    }
}

