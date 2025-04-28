using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransCaseRefund
{
    public Guid Id { get; set; }

    public Guid TransCaseId { get; set; }

    public Guid TransClientId { get; set; }

    public decimal Amount { get; set; }

    public string MasterStatusCode { get; set; } = null!;

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public string? Remark { get; set; }

    public virtual MasterStatus MasterStatusCodeNavigation { get; set; } = null!;

    public virtual TransCase TransCase { get; set; } = null!;

    public virtual TransClient TransClient { get; set; } = null!;
}
