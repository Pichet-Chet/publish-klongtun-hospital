using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.Report
{
    public class ConsultStaffCaseReportResponse
    {
        public ConsultStaffCaseReportResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ConsultStaffCaseReportResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class ConsultStaffCaseReportResponseData
    {
        public string ColA { get; set; } = null!;

        public int ColB { get; set; }

        public int ColC { get; set; }

        public int ColD { get; set; }

        public int ColE { get; set; }

        public int ColF { get; set; }

        public int ColG { get; set; }

    }
}

