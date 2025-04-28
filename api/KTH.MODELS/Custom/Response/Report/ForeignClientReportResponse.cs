using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.Report
{
    public class ForeignClientReportResponse
    {
        public ForeignClientReportResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<ForeignClientReportResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class ForeignClientReportResponseData
    {
        public DateOnly ColA { get; set; }

        public string ColB { get; set; } = null!;

        public string ColC { get; set; } = null!;

        public string ColD { get; set; } = null!;

        public decimal ColE { get; set; }

        public decimal ColF { get; set; }

        public decimal ColG { get; set; }

        public decimal ColH { get; set; }

    }
}

