using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransCaseCancel
{
    public Guid Id { get; set; }

    public Guid TransCaseId { get; set; }

    public Guid MasterReasonNotTreatmentId { get; set; }

    public string? Remark { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual MasterReasonNotTreatment MasterReasonNotTreatment { get; set; } = null!;

    public virtual TransCase TransCase { get; set; } = null!;
}
