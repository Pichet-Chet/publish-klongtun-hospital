using System;
using System.Text.Json.Serialization;

namespace KTH.MODELS.Custom.Response.Report.Executive
{
    public class MonthlyPatientByStoreReportResponse
    {
        public MonthlyPatientByStoreReportResponse()
        {
            MessageAlert = new MessageAlertResponse();
        }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public MonthlyPatientByStoreReportResponseData Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class MonthlyPatientByStoreReportResponseData
    {
        public MonthlyPatientByStoreReportResponseData()
        {
            PatientIncomes = new List<PatientIncome>();
            PatientAccepts = new List<PatientAccept>();
            PatientAppointments = new List<PatientAppointment>();
        }

        public List<PatientIncome> PatientIncomes { get; set; }
        public List<PatientAccept> PatientAccepts { get; set; }
        public List<PatientAppointment> PatientAppointments { get; set; }
    }

    public class PatientIncome //   คนไข้เข้า
    {
        public string? ColA { get; set; } //ช่องทาง

        public int ColB { get; set; } // เดือน ม.ค.

        public int ColC { get; set; } // เดือน ก.พ.

        public int ColD { get; set; } // เดือน มี.ค.

        public int ColE { get; set; } // เดือน เม.ย.

        public int ColF { get; set; } // เดือน พ.ค.

        public int ColG { get; set; } // เดือน มิ.ย.

        public int ColH { get; set; } // เดือน ก.ค.

        public int ColI { get; set; } // เดือน ส.ค.

        public int ColJ { get; set; } // เดือน ก.ย.

        public int ColK { get; set; } // เดือน ต.ค.

        public int ColL { get; set; } // เดือน พ.ศ.

        public int ColM { get; set; } // เดือน ธ.ค.

        public int ColN { get; set; } // รวม


    }

    public class PatientAccept //   คนไข้ตกลงทำ
    {
        public string? ColA { get; set; } //ช่องทาง

        public int ColB { get; set; } // เดือน ม.ค.

        public int ColC { get; set; } // เดือน ก.พ.

        public int ColD { get; set; } // เดือน มี.ค.

        public int ColE { get; set; } // เดือน เม.ย.

        public int ColF { get; set; } // เดือน พ.ค.

        public int ColG { get; set; } // เดือน มิ.ย.

        public int ColH { get; set; } // เดือน ก.ค.

        public int ColI { get; set; } // เดือน ส.ค.

        public int ColJ { get; set; } // เดือน ก.ย.

        public int ColK { get; set; } // เดือน ต.ค.

        public int ColL { get; set; } // เดือน พ.ศ.

        public int ColM { get; set; } // เดือน ธ.ค.

        public int ColN { get; set; } // รวม

    }

    public class PatientAppointment //   คนไข้นัด
    {
        public string? ColA { get; set; } //ช่องทาง

        public int ColB { get; set; } // เดือน ม.ค.

        public int ColC { get; set; } // เดือน ก.พ.

        public int ColD { get; set; } // เดือน มี.ค.

        public int ColE { get; set; } // เดือน เม.ย.

        public int ColF { get; set; } // เดือน พ.ค.

        public int ColG { get; set; } // เดือน มิ.ย.

        public int ColH { get; set; } // เดือน ก.ค.

        public int ColI { get; set; } // เดือน ส.ค.

        public int ColJ { get; set; } // เดือน ก.ย.

        public int ColK { get; set; } // เดือน ต.ค.

        public int ColL { get; set; } // เดือน พ.ศ.

        public int ColM { get; set; } // เดือน ธ.ค.

        public int ColN { get; set; } // รวม

    }
}

