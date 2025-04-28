using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class MasterHospital
{
    public Guid Id { get; set; }

    public string NameTh { get; set; } = null!;

    public string? NameEn { get; set; }

    public int? Code { get; set; }

    public string? Department { get; set; }

    public string? Type { get; set; }

    public bool IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? MasterThaiProvincesNameTh { get; set; }

    public int? MasterThaiProvincesNameId { get; set; }
}
