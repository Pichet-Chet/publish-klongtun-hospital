using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.REPOSITORIES.Model.Report
{
    public class GetMonthlyAppointmentReportModel
    {
        public int CountData { get; set; }
        public List<GetMonthlyAppointmentReportModelData> Data {  get; set; }
    }

    public class GetMonthlyAppointmentReportModelData
    {
        public Guid Id { get; set; }
        public bool IsNewAppointment { get; set; }
        public Guid MasterReasonNewAppointmentId { get; set; }
        public DateTime StartConsultDate {  get; set; }
        public string MasterStatusCode {  get; set; }
    }
}
