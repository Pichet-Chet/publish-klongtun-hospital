using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransR8
{
    public Guid Id { get; set; }

    public Guid CaseId { get; set; }

    public bool TestResult { get; set; }

    public bool? IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual TransCase Case { get; set; } = null!;
}
