using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransOrder
{
    public Guid Id { get; set; }

    /// <summary>
    /// รายการเคสของคนไข้
    /// </summary>
    public Guid TransCaseId { get; set; }

    public string? RemarkOrder { get; set; }

    public string? RemarkSpecialDiscountRequest { get; set; }

    public string? RemarkSpecialDiscountApprove { get; set; }

    public string MasterStatusCode { get; set; } = null!;

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string CreatedBySysRoleCode { get; set; } = null!;

    public string? CreatedBySysRoleName { get; set; }

    public string OrderNo { get; set; } = null!;

    public Guid? StaffApproveId { get; set; }

    public virtual SysRole CreatedBySysRoleCodeNavigation { get; set; } = null!;

    public virtual MasterStatus MasterStatusCodeNavigation { get; set; } = null!;

    public virtual TransStaff? StaffApprove { get; set; }

    public virtual TransCase TransCase { get; set; } = null!;

    public virtual ICollection<TransOrderItem> TransOrderItems { get; set; } = new List<TransOrderItem>();

    public virtual ICollection<TransPaymentHeader> TransPaymentHeaders { get; set; } = new List<TransPaymentHeader>();
}
