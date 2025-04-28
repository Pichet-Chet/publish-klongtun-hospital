using System;
using KTH.MODELS.Custom.Response.SysRole;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.TransActionHistory
{
    public class TransActionHistoryListResponse
    {
        public TransActionHistoryListResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<TransActionHistoryListResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class TransActionHistoryListResponseData
	{
        public Guid Id { get; set; }

        public Guid TransCaseId { get; set; }

        public Guid TransCaseStatus { get; set; }

        public string ActionName { get; set; } = null!;

        public string ActionBy { get; set; } = null!;

        public DateTime ActionDate { get; set; }
    }
}

