using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.SummaryPeriodIncome
{
    public class DocumentCoverSheetRefferalFeeGenerateResponse
    {
        public DocumentCoverSheetRefferalFeeGenerateResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        // Include JsonIgnore condition to handle null values properly.
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DocumentCoverSheetRefferalFeeGenerateResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class DocumentCoverSheetRefferalFeeGenerateResponseData
    {
        // A list to hold multiple byte arrays representing the document images.
        public List<byte[]> StreamResults { get; set; } = new List<byte[]>();
    }
}
