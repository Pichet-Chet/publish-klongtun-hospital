using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransConsultComment
{
    public Guid Id { get; set; }

    public Guid TransCaseId { get; set; }

    public string Description { get; set; } = null!;

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public bool IsPtChecked { get; set; }

    public virtual TransCase TransCase { get; set; } = null!;
}
