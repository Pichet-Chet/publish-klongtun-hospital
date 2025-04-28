using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransPaymentHeader
{
    public Guid Id { get; set; }

    public string TransactionNo { get; set; } = null!;

    public DateTime TransactionDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string? Remark { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public bool IsReceipt { get; set; }

    public Guid? TransOrderId { get; set; }

    public string MasterStatusCode { get; set; } = null!;

    public Guid TransClientId { get; set; }

    /// <summary>
    /// 1=รับเงินโรงบาล
    /// 2=สมาคม
    /// 3=คืนเงินสำรองจ่าย
    /// </summary>
    public short MoneyBucket { get; set; }

    public Guid TransCaseId { get; set; }

    public string TransCaseNo { get; set; } = null!;

    public decimal PaymentCash { get; set; }

    public decimal PaymentQrCode { get; set; }

    public decimal PaymentCredit { get; set; }

    public string? PaymentCreditCard { get; set; }

    public string TypePaymet { get; set; } = null!;

    public bool IsCloseBalance { get; set; }

    /// <summary>
    /// วันที่บัญชีคืนเงินให้การเงิน
    /// </summary>
    public DateTime? AccountingRefundDate { get; set; }

    public Guid? AccountingRefundBy { get; set; }

    public virtual TransStaff? AccountingRefundByNavigation { get; set; }

    public virtual TransCase TransCase { get; set; } = null!;

    public virtual TransClient TransClient { get; set; } = null!;

    public virtual ICollection<TransClosePeriodIncomeDetail> TransClosePeriodIncomeDetails { get; set; } = new List<TransClosePeriodIncomeDetail>();

    public virtual TransOrder? TransOrder { get; set; }

    public virtual ICollection<TransPaymentDetail> TransPaymentDetails { get; set; } = new List<TransPaymentDetail>();
}
