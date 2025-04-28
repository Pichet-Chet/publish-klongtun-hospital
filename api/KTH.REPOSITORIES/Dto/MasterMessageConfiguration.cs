using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class MasterMessageConfiguration
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string MessageTh { get; set; } = null!;

    public string MessageEn { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }
}
