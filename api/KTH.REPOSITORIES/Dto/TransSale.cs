using System;
using System.Collections.Generic;

namespace KTH.REPOSITORIES.Dto;

public partial class TransSale
{
    public Guid Id { get; set; }

    public Guid MasterSaleGroupId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? NickName { get; set; }

    public string? AccessToken { get; set; }

    public DateTime? AccessTokenExpire { get; set; }

    public bool IsActive { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedDate { get; set; }

    public string RefCode { get; set; } = null!;

    public virtual MasterSaleGroup MasterSaleGroup { get; set; } = null!;

    public virtual ICollection<TransCase> TransCases { get; set; } = new List<TransCase>();

    public virtual ICollection<TransClient> TransClients { get; set; } = new List<TransClient>();

    public virtual ICollection<TransReferralFee> TransReferralFees { get; set; } = new List<TransReferralFee>();
}
