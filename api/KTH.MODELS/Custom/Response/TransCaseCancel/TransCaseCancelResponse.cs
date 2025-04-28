using System;
using System.Text.Json.Serialization;
using KTH.MODELS.Custom.Response.MasterReasonNotTreatment;
using KTH.MODELS.Custom.Response.SysConfiguration;
using KTH.MODELS.Custom.Response.TransCase;

namespace KTH.MODELS.Custom.Response.TransCaseCancel
{
    public class TransCaseCancelResponse
    {
        public TransCaseCancelResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TransCaseCancelResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class TransCaseCancelResponseData
	{
        public Guid Id { get; set; }

        public Guid TransCaseId { get; set; }

        public Guid MasterReasonNotTreatmentId { get; set; }

        public string? Remark { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}

