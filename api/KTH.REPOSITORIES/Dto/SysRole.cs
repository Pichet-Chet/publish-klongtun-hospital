using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class SysRole
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string Code { get; set; } = null!;

    public virtual ICollection<SysPermission> SysPermissions { get; set; } = new List<SysPermission>();

    public virtual ICollection<TransOrder> TransOrders { get; set; } = new List<TransOrder>();

    public virtual ICollection<TransStaff> TransStaffs { get; set; } = new List<TransStaff>();
}
