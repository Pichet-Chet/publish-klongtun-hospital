using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransCase
{
    public class GetTransCaseWithDateResponse
    {
        public List<GetTransCaseWithDateResponseData> Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetTransCaseWithDateResponseData
    {
        public GetTransCaseWithDateResponseData()
        {
            TransClientData = new TransClientFilterWithDateData();
        }

        public Guid Id { get; set; }
        public DateOnly? LastMonthlyPeriod { get; set; }
        public string? CaseNo { get; set; }
        public Guid? MasterConsultRoomId { get; set; }
        public int? GestationalAge { get; set; }
        public bool HistoryOfCesareanSection { get; set; }
        public bool MarriedOrBoyfriend { get; set; }
        public string? DrugAllergy { get; set; }
        public string? CongenitalDisease { get; set; }
        public string? ReasonTermination { get; set; }
        public string? InformationToDoctor { get; set; }
        public string? SaleRecord { get; set; }
        public DateOnly? ReceiveServiceDate { get; set; }
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public Guid? TransClientId { get; set; }
        public string MasterStatusCode { get; set; } = null!;
        public string MasterStatusName { get; set; } = null!;
        public string? Remark { get; set; }
        public int? HowManyTimes { get; set; }
        public int? NumberOfChildren { get; set; }

        public string? UsRemark { get; set; }
        public DateTime? UsDateTime { get; set; }

        public bool? IsNewAppointment { get; set; }

        public bool? IsFreeUs { get; set; }

        public bool? IsFreePt { get; set; }

        public string? Badge { get; set; } // ป้ายหมายเหตุ

        public bool IsRefund { get; set; }

        public TransClientFilterWithDateData TransClientData { get; set; }

    }

    public class TransClientFilterWithDateData
    {
        public string ClientNo { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string HnNo { get; set; } = null!;
        public string Age { get; set; } = null!;
        public string SaleName { get; set; } = null!;
        public string SaleGroup { get; set; } = null!;
    }
}
