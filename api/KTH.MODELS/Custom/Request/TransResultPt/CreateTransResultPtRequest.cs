using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.TransResultPt
{
    public class CreateTransResultPtRequest
    {
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string CaseId { get; set; }
        public bool ResultPt { get; set; }
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string CreatedBy { get; set; }
    }
}
