using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransReferralFee
{
    public class GetTransReferralFilterRequest : FilterModel
    {
        public string? SaleId {  get; set; }
        public decimal? TotalPrice { get; set; }
        public decimal? TotalCredit { get; set; }
        public string ? StatusCode { get; set; }
    }
}
