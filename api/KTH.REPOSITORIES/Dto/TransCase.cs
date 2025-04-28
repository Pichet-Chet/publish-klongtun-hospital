using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransCase
{
    public Guid Id { get; set; }

    /// <summary>
    /// ประจำเดือนล่าสุด
    /// </summary>
    public DateOnly? LastMonthlyPeriod { get; set; }

    /// <summary>
    /// อายุครรภ์
    /// </summary>
    public int? GestationalAge { get; set; }

    /// <summary>
    /// ประวัติการผ่าคลอด
    /// </summary>
    public bool HistoryOfCesareanSection { get; set; }

    /// <summary>
    /// แต่งงาน หรือ มีแฟน
    /// </summary>
    public bool MarriedOrBoyfriend { get; set; }

    /// <summary>
    /// ประวัติการแพ้ยา
    /// </summary>
    public string? DrugAllergy { get; set; }

    /// <summary>
    /// โรคประจำตัว
    /// </summary>
    public string? CongenitalDisease { get; set; }

    /// <summary>
    /// เหตุผลการยุติฯ
    /// </summary>
    public string? ReasonTermination { get; set; }

    /// <summary>
    /// ข้อมูลเพิ่มเติมถึงแพทย์
    /// </summary>
    public string? InformationToDoctor { get; set; }

    /// <summary>
    /// บันทึกของทีมขาย
    /// </summary>
    public string? SaleRecord { get; set; }

    /// <summary>
    /// วันที่ต้องการเข้ารับบริการ
    /// </summary>
    public DateOnly ReceiveServiceDate { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    /// <summary>
    /// รหัสลูกค้า
    /// </summary>
    public Guid TransClientId { get; set; }

    /// <summary>
    /// รหัสเคส
    /// </summary>
    public string CaseNo { get; set; } = null!;

    public string MasterStatusCode { get; set; } = null!;

    public Guid? MasterConsultRoomId { get; set; }

    public string? Remark { get; set; }

    /// <summary>
    /// เป็นการตั้งครรภ์ครั้งที่
    /// </summary>
    public int? HowManyTimes { get; set; }

    /// <summary>
    /// จำนวนบุตร
    /// </summary>
    public int? NumberOfChildren { get; set; }

    /// <summary>
    /// วันเวลาที่เริ่มเข้าห้องปรึกษา
    /// </summary>
    public DateTime? StartConsultDate { get; set; }

    /// <summary>
    /// หมายเหตุ เอาไว้ให้ห้อง US ดูเองเท่านั้น
    /// </summary>
    public string? StartConsultRemark { get; set; }

    /// <summary>
    /// เคสมีการคิดค่านำพาแล้วหรือยัง
    /// </summary>
    public bool IsCarryingCost { get; set; }

    public string? UsRemark { get; set; }

    public DateTime? UsDateTime { get; set; }

    /// <summary>
    /// เคสสำหรับลูกค้าต่างชาติ
    /// </summary>
    public bool? IsForeigner { get; set; }

    /// <summary>
    /// เคสนัดหมายใหม่ไหม
    /// </summary>
    public bool? IsNewAppointment { get; set; }

    /// <summary>
    /// ฟรี US หรือไม่
    /// </summary>
    public bool? IsFreeUs { get; set; }

    /// <summary>
    /// ฟรี PT หรือไม่่
    /// </summary>
    public bool? IsFreePt { get; set; }

    /// <summary>
    /// เคสกินยาหรือไม่่่
    /// </summary>
    public bool? IsTakeMedicine { get; set; }

    /// <summary>
    /// เคสนอนเลยไหม
    /// </summary>
    public bool? IsSleepNow { get; set; }

    /// <summary>
    /// เหตุผลนัดหมายใหม่
    /// </summary>
    public string? ReasonNewAppointment { get; set; }

    /// <summary>
    /// หมายเหตุนัดหมายใหม่
    /// </summary>
    public Guid? MasterReasonNewAppointmentId { get; set; }

    /// <summary>
    /// ช่องทางการรับรู้ข่าวสาร
    /// </summary>
    public Guid? MasterChannelInformationId { get; set; }

    public bool? IsSaleFollow { get; set; }

    /// <summary>
    /// รหัสพนักงานขายของทีมขาย
    /// </summary>
    public Guid TransSaleId { get; set; }

    /// <summary>
    /// รหัสทีมขาย
    /// </summary>
    public Guid MasterSaleGroupId { get; set; }

    public Guid? MasterReasonUnfollowId { get; set; }

    public string? RemarkUnfollow { get; set; }

    public bool? IsWalkin { get; set; }

    public Guid? NewAppointmentCaseId { get; set; }

    public Guid? UsByStaffId { get; set; }

    public string? PtRemark { get; set; }

    public DateTime? PtDatetime { get; set; }

    public Guid? PtByStaffId { get; set; }

    public bool? IsPt { get; set; }

    public Guid? MasterGestationalAgeId { get; set; }

    public Guid? ConsultByStaffId { get; set; }

    public DateTime? ConsultByDate { get; set; }

    public Guid? TransAttachmentId { get; set; }

    public virtual TransStaff? ConsultByStaff { get; set; }

    public virtual MasterChannelInformation? MasterChannelInformation { get; set; }

    public virtual MasterConsultRoom? MasterConsultRoom { get; set; }

    public virtual MasterReasonNewAppointment? MasterReasonNewAppointment { get; set; }

    public virtual MasterReasonUnFollow? MasterReasonUnfollow { get; set; }

    public virtual MasterSaleGroup MasterSaleGroup { get; set; } = null!;

    public virtual MasterStatus MasterStatusCodeNavigation { get; set; } = null!;

    public virtual ICollection<TransActionHistory> TransActionHistories { get; set; } = new List<TransActionHistory>();

    public virtual TransAttachment? TransAttachment { get; set; }

    public virtual TransCaseCancel? TransCaseCancel { get; set; }

    public virtual ICollection<TransCaseRefund> TransCaseRefunds { get; set; } = new List<TransCaseRefund>();

    public virtual TransClient TransClient { get; set; } = null!;

    public virtual ICollection<TransConsultComment> TransConsultComments { get; set; } = new List<TransConsultComment>();

    public virtual TransConsultRoom? TransConsultRoom { get; set; }

    public virtual ICollection<TransLab> TransLabs { get; set; } = new List<TransLab>();

    public virtual ICollection<TransLr> TransLrs { get; set; } = new List<TransLr>();

    public virtual ICollection<TransOrder> TransOrders { get; set; } = new List<TransOrder>();

    public virtual ICollection<TransPaymentHeader> TransPaymentHeaders { get; set; } = new List<TransPaymentHeader>();

    public virtual TransR8? TransR8 { get; set; }

    public virtual ICollection<TransReferralFee> TransReferralFees { get; set; } = new List<TransReferralFee>();

    public virtual ICollection<TransResultPt> TransResultPts { get; set; } = new List<TransResultPt>();

    public virtual TransSale TransSale { get; set; } = null!;
}
