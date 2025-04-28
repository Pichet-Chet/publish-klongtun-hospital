using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransCaseRefundOverdue1663
{
    public Guid Id { get; set; }

    public int Month { get; set; }

    public int Year { get; set; }

    public decimal Price { get; set; }

    public decimal Paid { get; set; }

    public decimal Overdue { get; set; }

    public DateTime? RefundDatetime { get; set; }

    public bool IsRefunded { get; set; }

    public Guid? TransClosePeriodIncomeHeaderId { get; set; }

    /// <summary>
    /// Cashier
    /// </summary>
    public Guid? TransStaffId { get; set; }

    public virtual TransClosePeriodIncomeHeader? TransClosePeriodIncomeHeader { get; set; }

    public virtual TransStaff? TransStaff { get; set; }
}
