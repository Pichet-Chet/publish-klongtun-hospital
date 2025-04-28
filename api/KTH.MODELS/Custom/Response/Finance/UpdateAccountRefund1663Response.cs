using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.Finance
{
    public class UpdateAccountRefund1663Response
    {
        public UpdateAccountRefund1663ResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }
    public class UpdateAccountRefund1663ResponseData
    {
        public Guid Id { get; set; }
    }
}
