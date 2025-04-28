using KTH.MODELS.Custom.Response.MasterCountry;
using KTH.MODELS.Helper;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using static KTH.MODELS.Constants.ConstantsMassage;

namespace KTH.MODELS.Custom.Request.TransClient
{
    public class CreateTransClientRequest
    {
        // Ref Code 

        public string? TranSaleRefCode { get; set; }


        // Part 1

        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; } = null!;

        public string? CitizenIdentification { get; set; }

        public string? PassportNumber { get; set; }

        public string? Address { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required")]
        public DateOnly? DateOfBirth { get; set; }

        [Required(ErrorMessage = "TelephoneNumber is required")]
        public string TelephoneNumber { get; set; } = null!;

        public string? MasterNationalityId { get; set; }

        [Required(ErrorMessage = "IsActive is required")]
        public bool IsActive { get; set; } = true;

        public string? CreatedBy { get; set; }

        [Required(ErrorMessage = "TelephoneCode is required")]
        public string TelephoneCode { get; set; } = null!;

        public string? TelephoneSecond { get; set; }

        public string? MasterRightTreatmentId { get; set; }

        public string? HnNo { get; set; }

        public string? Occupation { get; set; }

        public string? HostpitalName { get; set; }


        // Part 2

        public DateOnly LastMonthlyPeriod { get; set; }

        public int GestationalAge { get; set; }

        public bool HistoryOfCesareanSection { get; set; }

        public bool MarriedOrBoyfriend { get; set; }

        public string? DrugAllergy { get; set; }

        public string? CongenitalDisease { get; set; }

        public string? ReasonTermination { get; set; }

        public string? InformationToDoctor { get; set; }

        // Part 3

        [Required(ErrorMessage = "ReceiveServiceDate is required")]
        public DateOnly? ReceiveServiceDate {  get; set; }
    }
}
