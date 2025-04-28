using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.Finance
{
    public class AccountRefundFinanceRequest : FilterModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set;}
        public string? Status {  get; set; }
    }
}
