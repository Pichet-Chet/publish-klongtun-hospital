using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class MasterNationality
{
    public Guid Id { get; set; }

    public string NameTh { get; set; } = null!;

    public string? NameEn { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public string? Code { get; set; }

    public virtual ICollection<TransClient> TransClients { get; set; } = new List<TransClient>();
}
