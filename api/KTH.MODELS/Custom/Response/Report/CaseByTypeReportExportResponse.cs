using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.Report
{
    public class CaseByTypeReportExportResponse
    {
        public MemoryStream Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }
}
