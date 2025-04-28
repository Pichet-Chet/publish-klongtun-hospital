using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class MasterReferralFrom
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<TransConsultRoom> TransConsultRooms { get; set; } = new List<TransConsultRoom>();

    public virtual ICollection<TransReferralFee> TransReferralFees { get; set; } = new List<TransReferralFee>();
}
