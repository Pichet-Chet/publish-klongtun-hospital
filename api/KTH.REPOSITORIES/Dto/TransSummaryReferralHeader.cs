using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransSummaryReferralHeader
{
    public Guid Id { get; set; }

    public string SummaryHeaderNo { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal TotalCase { get; set; }

    public decimal TotalReferral { get; set; }

    public decimal TotalCredit { get; set; }

    public string MasterStatusCode { get; set; } = null!;

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? ApproverBy { get; set; }

    public DateTime? ApproverDate { get; set; }

    public bool? IsPrint { get; set; }

    public virtual ICollection<TransSummaryReferralDetail> TransSummaryReferralDetails { get; set; } = new List<TransSummaryReferralDetail>();
}
