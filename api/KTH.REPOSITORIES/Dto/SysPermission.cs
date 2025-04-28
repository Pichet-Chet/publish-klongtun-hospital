using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class SysPermission
{
    public Guid Id { get; set; }

    public Guid SysRoleId { get; set; }

    public string Page { get; set; } = null!;

    public string? Action { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual SysRole SysRole { get; set; } = null!;
}
