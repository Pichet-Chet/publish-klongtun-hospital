using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransAssignAssistantManager
{
    public Guid Id { get; set; }

    public Guid TransStaffId { get; set; }

    public string StaffName { get; set; } = null!;

    public string? Reason { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual TransStaff TransStaff { get; set; } = null!;
}
