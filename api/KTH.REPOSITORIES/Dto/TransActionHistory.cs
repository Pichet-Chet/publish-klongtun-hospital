using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransActionHistory
{
    public Guid Id { get; set; }

    public Guid TransCaseId { get; set; }

    public Guid TransCaseStatus { get; set; }

    public string? ActionName { get; set; }

    public string? ActionBy { get; set; }

    public DateTime ActionDate { get; set; }

    public virtual TransCase TransCase { get; set; } = null!;
}
