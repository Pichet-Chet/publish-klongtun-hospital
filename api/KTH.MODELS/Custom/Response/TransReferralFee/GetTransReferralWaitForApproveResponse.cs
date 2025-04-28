using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransReferralFee
{
    public class GetTransReferralWaitForApproveResponse
    {
        public List<GetTransReferralWaitForApproveResponseData> Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetTransReferralWaitForApproveResponseData
    {
        public Guid Id { get; set; }
        public Guid CaseId { get; set; }
        public string CaseNo { get; set; }
        public string ClientName { get; set; }
        public string StartConsultDate { get; set; }
        public string SaleName { get; set; }
        public string ReferralName { get; set; }
        public decimal ReferralFee { get; set; }
        public decimal Credit { get; set; }
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
        public decimal? SaleAmount { get; set; }
        public decimal? ReferralAmount { get; set; }
        public bool IsCompleted { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid? ApproveIdLv1 { get; set; }
        public Guid? ApproveIdLv2 { get; set; }
    }
}
