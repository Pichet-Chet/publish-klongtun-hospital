using System;
using KTH.MODELS.Custom.Response.MasterReasonNotTreatment;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.MasterRightTreatment
{
    public class MasterRightTreatmentListResponse
    {
        public MasterRightTreatmentListResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<MasterRightTreatmentListResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MasterRightTreatmentListResponseData
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }
    }
}

