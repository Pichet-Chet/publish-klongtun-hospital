using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransOrderItem
{
    public Guid Id { get; set; }

    /// <summary>
    /// รายการออเดอร์
    /// </summary>
    public Guid TransOrderId { get; set; }

    /// <summary>
    /// รายการสินค้า
    /// </summary>
    public Guid MasterItemOrderId { get; set; }

    /// <summary>
    /// สปสช จ่าย
    /// </summary>
    public decimal NhsoPaid { get; set; }

    /// <summary>
    /// ส่วนลดพิเศษ
    /// </summary>
    public decimal SpecialDiscountPaid { get; set; }

    /// <summary>
    /// เงินสงเคราะห์
    /// </summary>
    public decimal AidPaid { get; set; }

    /// <summary>
    /// 1663 จ่าย
    /// </summary>
    public decimal X1663Paid { get; set; }

    /// <summary>
    /// เงินสำรองจ่าย
    /// </summary>
    public decimal Reserve { get; set; }

    /// <summary>
    /// ยอดคงเหลือที่ต้องชำระ
    /// </summary>
    public decimal Remain { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool Paid { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public int Seq { get; set; }

    public virtual MasterItemsOrder MasterItemOrder { get; set; } = null!;

    public virtual TransOrder TransOrder { get; set; } = null!;

    public virtual ICollection<TransPaymentDetail> TransPaymentDetails { get; set; } = new List<TransPaymentDetail>();
}
