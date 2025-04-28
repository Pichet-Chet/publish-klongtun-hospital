using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransPayment
{
    public class GetTransPaymentFilterRequest : FilterModel
    {
        public string? transCaseId { get; set; }

        public string? transClientId { get; set; }

        public string? TypePayment { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int? MoneyBucket { get; set; }

        public bool? IsCloseBalance { get; set; }
    }
}
