using KTH.MODELS.Custom.Response.MasterRightTreatment;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.MasterSaleGroup
{
    public class TransCaseDocumentResponse
    {
        public TransCaseDocumentResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TransCaseDocumentResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class TransCaseDocumentResponseData
    {
        public byte[] StreamResult { get; set; }
    }
}

