using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class MasterThaiDistrict
{
    public int Id { get; set; }

    public string NameTh { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public int MasterThaiProvincesId { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public virtual MasterThaiProvince MasterThaiProvinces { get; set; } = null!;

    public virtual ICollection<MasterThaiSubdistrict> MasterThaiSubdistricts { get; set; } = new List<MasterThaiSubdistrict>();
}
