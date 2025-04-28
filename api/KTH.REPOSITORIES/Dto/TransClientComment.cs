using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransClientComment
{
    public Guid Id { get; set; }

    public Guid TransClientId { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual TransClient TransClient { get; set; } = null!;
}
