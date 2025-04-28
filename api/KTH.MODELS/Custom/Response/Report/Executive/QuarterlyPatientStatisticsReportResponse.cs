using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.Report.Executive
{
	public class QuarterlyPatientStatisticsReportResponse
	{
        public QuarterlyPatientStatisticsReportResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<QuarterlyPatientStatisticsReportResponseData>? Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class QuarterlyPatientStatisticsReportResponseData
    {
        public string? ColA { get; set; } // ไตรมาส

        public double ColB { get; set; } // คนไข้เข้า : 2567

        public double ColC { get; set; } // คนไข้เข้า : 2568

        public double ColD { get; set; } // คนไข้เข้า : 2569

        public double ColE { get; set; } // คนไข้เข้า : 2570

        public double ColF { get; set; } // คนไข้เข้า : 2571



        public double ColG { get; set; } // คนไข้ตกลงทำ : 2567

        public double ColH { get; set; } // คนไข้ตกลงทำ : 2568

        public double ColI { get; set; } // คนไข้ตกลงทำ : 2569

        public double ColJ { get; set; } // คนไข้ตกลงทำ : 2570

        public double ColK { get; set; } // คนไข้ตกลงทำ : 2571



        public double ColL { get; set; } // คนไข้เก่ากลับมาใหม่ : 2567

        public double ColM { get; set; } // คนไข้เก่ากลับมาใหม่ : 2568

        public double ColN { get; set; } // คนไข้เก่ากลับมาใหม่ : 2569

        public double ColO { get; set; } // คนไข้เก่ากลับมาใหม่ : 2570

        public double ColP { get; set; } // คนไข้เก่ากลับมาใหม่ : 2571

    }
}

