using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.Report.Executive
{
	public class MonthlyStoreCountAndCarriageFeeReportResponse
	{
        public MonthlyStoreCountAndCarriageFeeReportResponse()
        {
            MessageAlert = new MessageAlertResponse();
            Data = new MonthlyStoreCountAndCarriageFeeReportResponseData();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MonthlyStoreCountAndCarriageFeeReportResponseData Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }


    public class MonthlyStoreCountAndCarriageFeeReportResponseData
    {
        public MonthlyStoreCountAndCarriageFeeReportResponseData()
        {
            Yearlies = new List<Yearly>();
        }

        public List<Yearly>  Yearlies { get; set; }
    }

    public class YearlyDataResponse
    {
        public string? ColA { get; set; } // รายละเอียด

        public double ColB { get; set; } // เดือน ม.ค.

        public double ColC { get; set; } // เดือน ก.พ.

        public double ColD { get; set; } // เดือน มี.ค.

        public double ColE { get; set; } // เดือน เม.ย.

        public double ColF { get; set; } // เดือน พ.ค.

        public double ColG { get; set; } // เดือน มิ.ย.

        public double ColH { get; set; } // เดือน ก.ค.

        public double ColI { get; set; } // เดือน ส.ค.

        public double ColJ { get; set; } // เดือน ก.ย.

        public double ColK { get; set; } // เดือน ต.ค.

        public double ColL { get; set; } // เดือน พ.ศ.

        public double ColM { get; set; } // เดือน ธ.ค.

        public double ColN { get; set; } // รวม
    }


    public class Yearly
    {
        public Yearly()
        {
            datas = new List<YearlyDataResponse>();
        }

        public string? Year { get; set; }

        public List<YearlyDataResponse> datas { get; set; }
    }
}

