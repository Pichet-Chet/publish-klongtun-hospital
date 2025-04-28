using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransClosePeriodIncomeDetail
{
    public Guid Id { get; set; }

    public Guid TransClosePeriodIncomeHeaderId { get; set; }

    public Guid? TransPaymentHeaderId { get; set; }

    /// <summary>
    /// เงินสด
    /// </summary>
    public decimal Cash { get; set; }

    /// <summary>
    /// สแกนจ่าย หรือ QR Code
    /// </summary>
    public decimal QrCode { get; set; }

    /// <summary>
    /// บัตรเครดิต
    /// </summary>
    public decimal CreditCard { get; set; }

    /// <summary>
    /// ยอดรวม
    /// </summary>
    public decimal Summary { get; set; }

    public bool IsRefund { get; set; }

    public Guid? TransCaseId { get; set; }

    /// <summary>
    /// หมายเลขบัตรเครดิต
    /// </summary>
    public string? CreditCardNumber { get; set; }

    /// <summary>
    /// เงินสำรองจ่าย
    /// </summary>
    public decimal X1663paid { get; set; }

    public virtual TransClosePeriodIncomeHeader TransClosePeriodIncomeHeader { get; set; } = null!;

    public virtual TransPaymentHeader? TransPaymentHeader { get; set; }
}
