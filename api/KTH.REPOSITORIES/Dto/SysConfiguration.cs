using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class SysConfiguration
{
    public Guid Id { get; set; }

    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? Group { get; set; }

    public string? Description { get; set; }
}
