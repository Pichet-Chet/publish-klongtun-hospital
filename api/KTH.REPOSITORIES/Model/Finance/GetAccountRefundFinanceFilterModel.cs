using KTH.REPOSITORIES.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.REPOSITORIES.Model.Finance
{
    public class GetAccountRefundFinanceFilterModel
    {
        public List<TransPaymentHeader> TransPaymentHeaderList { get; set; }
        public int Count { get; set; }
    }
}
