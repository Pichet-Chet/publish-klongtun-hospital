using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransReferralHistory
{
    public Guid Id { get; set; }

    public Guid TransReferralId { get; set; }

    public decimal OldReferralFee { get; set; }

    public decimal OldCredit { get; set; }

    public string OldRemark { get; set; } = null!;

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public decimal NewReferralFee { get; set; }

    public decimal NewCredit { get; set; }

    public string NewRemark { get; set; } = null!;

    public virtual TransReferralFee TransReferral { get; set; } = null!;
}
