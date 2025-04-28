using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class MasterChannelInformation
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? Seq { get; set; }

    public virtual ICollection<TransCase> TransCases { get; set; } = new List<TransCase>();

    public virtual ICollection<TransConsultRoom> TransConsultRooms { get; set; } = new List<TransConsultRoom>();
}
