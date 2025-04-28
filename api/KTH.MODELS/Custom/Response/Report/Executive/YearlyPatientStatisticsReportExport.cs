using System;
namespace KTH.MODELS.Custom.Response.Report.Executive
{
	public class YearlyPatientStatisticsReportExport
	{
        public MemoryStream Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }
}

