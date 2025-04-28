using System;
using KTH.MODELS.Custom.Response.MasterPhysician;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.MasterReasonNewAppointment
{
    public class MasterReasonNewAppointmentResponse
    {
        public MasterReasonNewAppointmentResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MasterReasonNewAppointmentResponseData? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MasterReasonNewAppointmentResponseData
    {
        public Guid Id { get; set; }

        public string Group { get; set; } = null!;

        public string Name { get; set; } = null!;

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }
    }
}

