using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransCase
{
    public class GetCaseForSaleResponse
    {
        public GetCaseForSaleResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }

    public class GetCaseForSaleResponseData
    {
        public int CountCaseFilter {  get; set; }
        public string UrlSale {  get; set; }
        public List<CaseDetail> CaseDetailData { get; set; }
    }



    public class CaseDetail
    {
        public Guid Id { get; set; }
        public string ReceiveServiceDate { get; set; }
        public string FullName { get; set; }
        public string RefCode { get; set; }
        public string StatusName { get; set; }
        public string TransCaseNo { get; set; }
        public string CreateDate { get; set; }
        public string ClientTel { get; set; }
        public string SaleRecord { get; set; }
        public bool IsRsa { get; set; }
        public string LastMonthlyPeriod { get; set; }
        public int? GestationalAge { get; set; }
        public string HistoryOfCesareanSection { get; set; }
        public string MarriedOrBoyfriend { get; set; }
        public string DrugAllergy { get; set; }
        public string CongenitalDisease { get; set; }
        public string ReasonTermination { get; set; }
        public string InformationToDoctor { get; set; }
        public string SaleFullName {  get; set; }
        public string TransSaleId { get; set; }
        public string UpdateDate { get; set; }
    }
}
