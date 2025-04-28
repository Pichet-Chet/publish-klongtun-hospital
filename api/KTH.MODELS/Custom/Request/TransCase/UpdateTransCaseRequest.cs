using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransCase
{
    public class UpdateTransCaseRequest
    {
        [Required(ErrorMessage = "Id is required")]
        public string Id { get; set; } = null!;

        public DateOnly? LastMonthlyPeriod { get; set; }

        public int? GestationalAge { get; set; }

        public bool HistoryOfCesareanSection { get; set; }

        [Required(ErrorMessage = "MarriedOrBoyfriend is required")]
        public bool MarriedOrBoyfriend { get; set; }

        public string? DrugAllergy { get; set; }

        public string? CongenitalDisease { get; set; }

        public string? ReasonTermination { get; set; }

        public string? InformationToDoctor { get; set; }

        public string? SaleRecord { get; set; }
        [Required(ErrorMessage = "ReceiveServiceDate is required")]
        public DateOnly? ReceiveServiceDate { get; set; }
        [Required(ErrorMessage = "IsActive is required")]
        public bool IsActive { get; set; }

        public string? UpdatedBy { get; set; }
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string TransClientId { get; set; }

        [Required(ErrorMessage = "MasterStatusCode is required")]
        public string MasterStatusCode { get; set; } = null!;
        public string? MasterConsultRoomId { get; set; }

        public string? Remark { get; set; }
        /// <summary>
        /// เป็นการตั้งครรภ์ครั้งที่
        /// </summary>
        public int? HowManyTimes { get; set; }

        /// <summary>
        /// จำนวนบุตร
        /// </summary>
        public int? NumberOfChildren { get; set; }
    }
}
