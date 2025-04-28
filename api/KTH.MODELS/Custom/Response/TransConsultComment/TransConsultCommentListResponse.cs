using System;
using System.Text.Json.Serialization;
using KTH.MODELS.Custom.Response.TransClient;
using KTH.MODELS.Custom.Response.TransClientComment;

namespace KTH.MODELS.Custom.Response.TransConsultComment
{
    public class TransConsultCommentListResponse
    {
        public TransConsultCommentListResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<TransConsultCommentListResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class TransConsultCommentListResponseData
	{

        public Guid Id { get; set; }

        public Guid TransCaseId { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}

