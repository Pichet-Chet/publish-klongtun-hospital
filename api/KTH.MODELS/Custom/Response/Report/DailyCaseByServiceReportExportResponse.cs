using System;
namespace KTH.MODELS.Custom.Response.Report
{
	public class DailyCaseByServiceReportExportResponse
	{
        public MemoryStream Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }
}

