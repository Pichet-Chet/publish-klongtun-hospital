using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class MasterItemsOrder
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    /// <summary>
    /// หน่วยเงินเป็นบาท
    /// </summary>
    public decimal Price { get; set; }

    public Guid? MasterItemsOrderGroupId { get; set; }

    public virtual MasterItemsOrderGroup? MasterItemsOrderGroup { get; set; }

    public virtual ICollection<TransLab> TransLabs { get; set; } = new List<TransLab>();

    public virtual ICollection<TransOrderItem> TransOrderItems { get; set; } = new List<TransOrderItem>();
}
