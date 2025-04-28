using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransAttachment
{
    public Guid Id { get; set; }

    public Guid ReferanceId { get; set; }

    public string SysType { get; set; } = null!;

    public string FileName { get; set; } = null!;

    public double FileSize { get; set; }

    public string FileSizeUnit { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public virtual ICollection<TransCase> TransCases { get; set; } = new List<TransCase>();
}
