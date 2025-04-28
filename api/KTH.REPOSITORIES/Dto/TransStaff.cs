using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransStaff
{
    public Guid Id { get; set; }

    public Guid SysRoleId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? NickName { get; set; }

    public string? AccessToken { get; set; }

    public DateTime? AccessTokenExpire { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsOnline { get; set; }

    public bool? IsDelete { get; set; }

    public virtual SysRole SysRole { get; set; } = null!;

    public virtual ICollection<TransAssignAssistantManager> TransAssignAssistantManagers { get; set; } = new List<TransAssignAssistantManager>();

    public virtual ICollection<TransCaseRefundOverdue1663> TransCaseRefundOverdue1663s { get; set; } = new List<TransCaseRefundOverdue1663>();

    public virtual ICollection<TransCase> TransCases { get; set; } = new List<TransCase>();

    public virtual ICollection<TransConsultRoom> TransConsultRooms { get; set; } = new List<TransConsultRoom>();

    public virtual ICollection<TransOrder> TransOrders { get; set; } = new List<TransOrder>();

    public virtual ICollection<TransPaymentHeader> TransPaymentHeaders { get; set; } = new List<TransPaymentHeader>();

    public virtual ICollection<TransReferralFee> TransReferralFeeApproverLv1s { get; set; } = new List<TransReferralFee>();

    public virtual ICollection<TransReferralFee> TransReferralFeeApproverLv2s { get; set; } = new List<TransReferralFee>();
}
