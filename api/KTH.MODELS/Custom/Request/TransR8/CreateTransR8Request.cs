using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.TransR8
{
    public class CreateTransR8Request
    {
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string CaseId { get; set; }
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string CreatedBy { get; set; }

        public bool TestResult { get; set; }
    }
}
