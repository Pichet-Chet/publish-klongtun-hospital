using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransCase
{
    public class GetTransCaseWithIdResponse
    {
        public GetTransCaseWithIdResponseData Data { get; set; }

        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetTransCaseWithIdResponseData
    {
        public GetTransCaseWithIdResponseData()
        {
            TransClientData = new TransClientWithIdData();
        }

        public Guid Id { get; set; }
        public string? CaseNo { get; set; }
        public DateOnly? LastMonthlyPeriod { get; set; }
        public bool MarriedOrBoyfriend { get; set; }

        public bool HistoryOfCesareanSection { get; set; }
        public int? GestationalAge { get; set; }
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
        public Guid? MasterConsultRoomId { get; set; }
        public string? MasterConsultRoomName { get; set; }

        public string MasterStatusCode { get; set; } = null!;
        public string MasterStatusName { get; set; } = null!;
        public string? Remark { get; set; } = null!;
        /// <summary>
        /// เป็นการตั้งครรภ์ครั้งที่
        /// </summary>
        public int? HowManyTimes { get; set; }

        /// <summary>
        /// จำนวนบุตร
        /// </summary>
        public int? NumberOfChildren { get; set; }

        public string? UsRemark { get; set; }

        public DateTime? UsDateTime { get; set; }

        public bool? IsNewAppointment { get; set; }

        public bool? IsFreeUs { get; set; }

        public bool? IsFreePt { get; set; }

        public string? Badge { get; set; } // ป้ายหมายเหตุ

        public bool IsRefund { get; set; }

        public Guid? NewAppointmentCaseId { get; set; }


        public Guid? UsByStaffId { get; set; }

        public string? PtRemark { get; set; }

        public DateTime? PtDatetime { get; set; }

        public Guid? PtByStaffId { get; set; }

        public Guid? MasterGestationalAgeId { get; set; }

        public TransClientWithIdData TransClientData { get; set; }
    }

    public class TransClientWithIdData
    {
        public string ClientNo { get; set; } = null!;
        public string Fullname { get; set; } = null!;
        public string HnNo { get; set; } = null!;
        public string Age { get; set; } = null!;
        public string SaleName { get; set; } = null!;
        public string SaleGroup { get; set; } = null!;
    }
}
