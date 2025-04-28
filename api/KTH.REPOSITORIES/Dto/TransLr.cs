using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransLr
{
    public Guid Id { get; set; }

    public Guid TransCaseId { get; set; }

    public Guid TransCliendId { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual TransCase TransCase { get; set; } = null!;

    public virtual TransClient TransCliend { get; set; } = null!;
}
