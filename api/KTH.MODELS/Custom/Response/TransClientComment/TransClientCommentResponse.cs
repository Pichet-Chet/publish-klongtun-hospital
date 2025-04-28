using System;
using KTH.MODELS.Custom.Response.TransActionHistory;
using System.Text.Json.Serialization;
using KTH.MODELS.Custom.Response.TransClient;

namespace KTH.MODELS.Custom.Response.TransClientComment
{
    public class TransClientCommentResponse
    {
        public TransClientCommentResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TransClientCommentResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class TransClientCommentResponseData
	{
        public Guid Id { get; set; }

        public Guid TransClientId { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public virtual GetTransClientWithIdResponse TransClient { get; set; } = null!;
    }
}

