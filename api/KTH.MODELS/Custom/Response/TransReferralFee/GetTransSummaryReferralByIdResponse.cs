using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransReferralFee
{
    public class GetTransSummaryReferralByIdResponse
    {
        public GetTransSummaryReferralByIdResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetTransSummaryReferralByIdResponseData
    {
        public Guid Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal TotalCase { get; set; }
        public decimal TotalReferral { get; set; }
        public decimal TotalCredit { get; set; }
        public string StatusCode { get; set; }
        public string StatusName { get; set; }
        public string HeaderNo { get; set; }
       public List<GetTransSummaryReferralByIdResponseDataSale> SaleData { get; set; }
    }

    public class GetTransSummaryReferralByIdResponseDataSale
    {
        public Guid SaleGroupId { get; set; }
        public string SaleGroupName { get; set; }
        public List<TransSummaryReferralDataItem> TransSummaryReferralItem { get; set; }
    }

    public class TransSummaryReferralDataItem
    {
        public Guid Id { get; set; }
        public Guid TransReferralId { get; set; }
        public string CreateDate { get; set; }
        public Guid CaseId { get; set; }
        public string CaseNo { get; set; }
        public Guid ClientId { get; set; }
        public string ClientName { get; set; }
        public Guid? SaleId { get; set; }
        public string? SaleName { get; set; }
        public Guid? MasterReferralId { get; set; }
        public string MasterReferralName { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ReferralFee { get; set; }
        public decimal Credit { get; set; }
        public string? Remark { get; set; }
        public decimal? SaleAmount { get; set; }
        public decimal? ReferralAmount { get; set; }
        public decimal TotalNhso { get; set; }

    }
}
