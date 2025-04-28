using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransCase
{
    public class UpdateRemarkSaleRequest
    {
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string CaseId { get; set; }
        public string Remark { get; set; }
    }
}
