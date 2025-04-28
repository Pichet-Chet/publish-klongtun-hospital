using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransClosePeriodIncomeHeader
{
    public Guid Id { get; set; }

    public DateTime PeriodStartDatetime { get; set; }

    public DateTime PeriodEndDatetime { get; set; }

    public string RoleName { get; set; } = null!;

    public string MoneyBucket { get; set; } = null!;

    public DateTime? SummaryDatetime { get; set; }

    public string? ActionName { get; set; }

    public string ClosePeriodNo { get; set; } = null!;

    public virtual ICollection<TransCaseRefundOverdue1663> TransCaseRefundOverdue1663s { get; set; } = new List<TransCaseRefundOverdue1663>();

    public virtual ICollection<TransClosePeriodIncomeDetail> TransClosePeriodIncomeDetails { get; set; } = new List<TransClosePeriodIncomeDetail>();
}
