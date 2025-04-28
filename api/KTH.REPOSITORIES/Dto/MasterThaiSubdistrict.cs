using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class MasterThaiSubdistrict
{
    public int Id { get; set; }

    public int? PostCode { get; set; }

    public string NameTh { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public int? MasterThaiDistrictsId { get; set; }

    public string? Description { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual MasterThaiDistrict? MasterThaiDistricts { get; set; }
}
