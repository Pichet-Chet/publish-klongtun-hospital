using System;
namespace KTH.MODELS.Custom.Response.Report.Executive
{
	public class MonthlyPatientStatisticsReportExport
	{
        public MemoryStream Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }
}

