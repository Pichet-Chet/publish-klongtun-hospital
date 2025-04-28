using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class MasterStatus
{
    public Guid Id { get; set; }

    public string Group { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? Seq { get; set; }

    public virtual ICollection<TransCaseRefund> TransCaseRefunds { get; set; } = new List<TransCaseRefund>();

    public virtual ICollection<TransCase> TransCases { get; set; } = new List<TransCase>();

    public virtual ICollection<TransOrder> TransOrders { get; set; } = new List<TransOrder>();
}
