using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.Finance
{
    public class UpdateAccountRefund1663Request
    {
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; }
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string TransStaffId { get; set; }
    }
}
