using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransClient
{
    public Guid Id { get; set; }

    /// <summary>
    /// ชื่อนาม-สกุล
    /// </summary>
    public string FullName { get; set; } = null!;

    /// <summary>
    /// เลขบัตรประชาชน
    /// </summary>
    public string? CitizenIdentification { get; set; }

    /// <summary>
    /// หนังสือเดินทาง
    /// </summary>
    public string? PassportNumber { get; set; }

    /// <summary>
    /// ที่อยู่
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// วัน เดือน ปีเกิด
    /// </summary>
    public DateOnly DateOfBirth { get; set; }

    /// <summary>
    /// เบอร์โทรศัพท์ที่ใช้ลงละเบียน
    /// </summary>
    public string? TelephoneNumber { get; set; }

    /// <summary>
    /// รหัสสัญชาติ
    /// </summary>
    public Guid? MasterNationalityId { get; set; }

    public string? AccessToken { get; set; }

    public DateTime? AccessTokenExpire { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// รหัสเบอร์โทร
    /// </summary>
    public string TelephoneCode { get; set; } = null!;

    /// <summary>
    /// เบอร์โทรสำรอง
    /// </summary>
    public string? TelephoneSecond { get; set; }

    public bool? IsDelete { get; set; }

    public DateTime? DeletedDate { get; set; }

    public string? DeletedBy { get; set; }

    /// <summary>
    /// สิทธิการรักษา
    /// </summary>
    public Guid? MasterRightTreatmentId { get; set; }

    /// <summary>
    /// รหัส Sale ค่านำพา
    /// </summary>
    public string TranSaleRefCode { get; set; } = null!;

    /// <summary>
    /// รหัสคนไข้
    /// </summary>
    public string? HnNo { get; set; }

    /// <summary>
    /// เลขที่เอกสาร
    /// </summary>
    public string? ClientNo { get; set; }

    /// <summary>
    /// อาชีพ
    /// </summary>
    public string? Occupation { get; set; }

    public string? HostpitalName { get; set; }

    public DateTime? TransSaleRefCodeStamp { get; set; }

    public virtual MasterNationality? MasterNationality { get; set; }

    public virtual TransSale TranSaleRefCodeNavigation { get; set; } = null!;

    public virtual ICollection<TransCaseRefund> TransCaseRefunds { get; set; } = new List<TransCaseRefund>();

    public virtual ICollection<TransCase> TransCases { get; set; } = new List<TransCase>();

    public virtual ICollection<TransClientComment> TransClientComments { get; set; } = new List<TransClientComment>();

    public virtual ICollection<TransLr> TransLrs { get; set; } = new List<TransLr>();

    public virtual ICollection<TransPaymentHeader> TransPaymentHeaders { get; set; } = new List<TransPaymentHeader>();

    public virtual ICollection<TransReferralFee> TransReferralFees { get; set; } = new List<TransReferralFee>();
}
