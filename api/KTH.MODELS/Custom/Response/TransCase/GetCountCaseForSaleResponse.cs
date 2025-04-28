using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransCase
{
    public class GetCountCaseForSaleResponse
    {
        public List<GetCountCaseForSaleResponseData> Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetCountCaseForSaleResponseData
    {
        public string SaleName { get; set; }
        public string CountJan { get; set; }
        public string CountFeb { get; set; }
        public string CountMar { get; set; }
        public string CountApr { get; set; }
        public string CountMay { get; set; }
        public string CountJun { get; set; }
        public string CountJul { get; set; }
        public string CountAug { get; set; }
        public string CountSep { get; set; }
        public string CountOct { get; set; }
        public string CountNov { get; set; }
        public string CountDec { get; set; }
    }
}
