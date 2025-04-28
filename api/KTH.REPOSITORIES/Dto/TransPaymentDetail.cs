using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransPaymentDetail
{
    public Guid Id { get; set; }

    public Guid TransTransactionPaymentHeaderId { get; set; }

    public Guid TransOrderItemId { get; set; }

    public decimal Amount { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public virtual TransOrderItem TransOrderItem { get; set; } = null!;

    public virtual TransPaymentHeader TransTransactionPaymentHeader { get; set; } = null!;
}
