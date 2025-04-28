using KTH.REPOSITORIES.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.REPOSITORIES.Model.TransCase
{
    public class GetTransCaseForSaleFilterModel
    {
       public List<GetTransCaseForSaleFilterModelData> Data { get; set; }
    }

    public class GetTransCaseForSaleFilterModelData
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateOnly ReceiveServiceDate {  get; set; }
        public string TransSaleRefCode {  get; set; }
        public DateTime CreatedDate {  get; set; }
        public string CaseNo {  get; set; }
        public string TransClientTelephone {  get; set; }
        public string? CongenitalDisease { get; set; }
        public string? DrugAllergy { get; set; }
        public int? GestationalAge {  get; set; }
        public bool HistoryOfCesareanSection { get; set; }
        public string? InformationToDoctor {  get; set; }
        public DateOnly? LastMonthlyPeriod {  get; set; }
        public bool MarriedOrBoyfriend {  get; set; }
        public string? ReasonTermination {  get; set; }
        public string? TransSaleFullName {  get; set; }
        public string? TransSaleId { get; set; }
        public DateTime UpdatedDate {  get; set; }
        public string MasterStatusCode {  get; set; }
        public bool? IsNewAppointment { get; set; }
        public string? SaleRecord {  get; set; }
    }

}
