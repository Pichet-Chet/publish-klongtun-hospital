using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransCase
{
    public class CountCaseForSaleResponse
    {
        public CountCase Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class CountCase
    {
        public int CountCaseYear { get; set; }
        public int CountCaseYearBySale { get; set; }
        public int CountCaseCureMonth { get; set; }
        public int CountCaseCureMonthBySale { get; set; }
        public string[] CountContract { get; set; }
        public string[] CountHealing { get; set; }
        public int MaxValueChart { get; set; }
    }
}
