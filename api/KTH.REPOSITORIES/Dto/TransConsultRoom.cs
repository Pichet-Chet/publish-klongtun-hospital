using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransConsultRoom
{
    public Guid Id { get; set; }

    public Guid TransCaseId { get; set; }

    /// <summary>
    /// เลือกอายุครร
    /// </summary>
    public Guid MasterGestationalAgeId { get; set; }

    /// <summary>
    /// เลือกใบส่งตัว
    /// </summary>
    public Guid? MasterReferralFromId { get; set; }

    public bool DrugAllergy { get; set; }

    public string? DrugAllergyRemark { get; set; }

    public bool CongenitalDisease { get; set; }

    public string? CongenitalDiseaseRemark { get; set; }

    public bool CaesareanSection { get; set; }

    public bool Relatives { get; set; }

    public bool Patient { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// คีย์เบิก
    /// </summary>
    public bool Withdraw { get; set; }

    /// <summary>
    /// ราคาต่างชาติหรือไม่
    /// </summary>
    public bool IsForeigner { get; set; }

    public Guid? MasterChannelInformationId { get; set; }

    public Guid TransStaffId { get; set; }

    public virtual MasterChannelInformation? MasterChannelInformation { get; set; }

    public virtual MasterGestationalAge MasterGestationalAge { get; set; } = null!;

    public virtual MasterReferralFrom? MasterReferralFrom { get; set; }

    public virtual TransCase TransCase { get; set; } = null!;

    public virtual TransStaff TransStaff { get; set; } = null!;
}
