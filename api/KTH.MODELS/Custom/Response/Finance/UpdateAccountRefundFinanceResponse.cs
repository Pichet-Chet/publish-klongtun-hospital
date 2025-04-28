using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.Finance
{
    public class UpdateAccountRefundFinanceResponse
    {
        public UpdateAccountRefundFinanceResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class UpdateAccountRefundFinanceResponseData
    {
        public Guid Id { get; set; }
    }
}
