using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransSummaryReferralDetail
{
    public Guid Id { get; set; }

    public Guid SummaryReferralHeaderId { get; set; }

    public Guid TransReferralFeeId { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual TransSummaryReferralHeader SummaryReferralHeader { get; set; } = null!;

    public virtual TransReferralFee TransReferralFee { get; set; } = null!;
}
