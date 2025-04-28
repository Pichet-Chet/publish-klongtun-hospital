using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransLab
{
    public Guid Id { get; set; }

    public Guid TransCaseId { get; set; }

    public string LabType { get; set; } = null!;

    public Guid MasterItemOrderId { get; set; }

    public bool IsSend { get; set; }

    public bool IsCompleted { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public virtual MasterItemsOrder MasterItemOrder { get; set; } = null!;

    public virtual TransCase TransCase { get; set; } = null!;
}
