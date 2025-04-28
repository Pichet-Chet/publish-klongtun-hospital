using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransReferralFee
{
    public Guid Id { get; set; }

    public Guid CaseId { get; set; }

    public string CaseNo { get; set; } = null!;

    public Guid ClientId { get; set; }

    public string ClientNo { get; set; } = null!;

    public DateTime StartConsultDate { get; set; }

    public Guid? SaleId { get; set; }

    public string? SaleFullname { get; set; }

    public Guid? MasterReferralFromId { get; set; }

    public decimal TotalPriceCase { get; set; }

    public decimal ReferralFee { get; set; }

    public decimal Credit { get; set; }

    public string? Remark { get; set; }

    public Guid? ApproverLv1Id { get; set; }

    public string? ApproverLv1Name { get; set; }

    public Guid? ApproverLv2Id { get; set; }

    public string? ApproverLv2Name { get; set; }

    public bool IsCompleted { get; set; }

    public bool IsClearCredit { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string MasterStatusCode { get; set; } = null!;

    public decimal? SaleAmount { get; set; }

    public decimal? ReferralAmount { get; set; }

    public decimal TotalNhso { get; set; }

    public DateTime? ApproverLv1Date { get; set; }

    public DateTime? ApproverLv2Date { get; set; }

    public bool? IsPrint { get; set; }

    public virtual TransStaff? ApproverLv1 { get; set; }

    public virtual TransStaff? ApproverLv2 { get; set; }

    public virtual TransCase Case { get; set; } = null!;

    public virtual TransClient Client { get; set; } = null!;

    public virtual MasterReferralFrom? MasterReferralFrom { get; set; }

    public virtual TransSale? Sale { get; set; }

    public virtual ICollection<TransReferralHistory> TransReferralHistories { get; set; } = new List<TransReferralHistory>();

    public virtual ICollection<TransSummaryReferralDetail> TransSummaryReferralDetails { get; set; } = new List<TransSummaryReferralDetail>();
}
