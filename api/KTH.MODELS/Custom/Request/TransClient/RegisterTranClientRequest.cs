using KTH.MODELS.Custom.Response.MasterCountry;
using KTH.MODELS.Helper;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;
using static KTH.MODELS.Constants.ConstantsMassage;

namespace KTH.MODELS.Custom.Request.TransClient
{
    public class RegisterTranClientRequest
    {
        // Ref Code 

        [Required]
        public string TranSaleRefCode { get; set; } = null!;


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

        [Required(ErrorMessage = "TelephoneCode is required")]
        [MaxLength(6)]
        public string TelephoneCode { get; set; } = null!;

        // Part 2

        public DateOnly? LastMonthlyPeriod { get; set; }

        public int GestationalAge { get; set; }

        public bool HistoryOfCesareanSection { get; set; }

        public bool MarriedOrBoyfriend { get; set; }

        public string? DrugAllergy { get; set; }

        public string? CongenitalDisease { get; set; }

        public string? ReasonTermination { get; set; }

        public string? InformationToDoctor { get; set; }

        // Part 3

        [Required(ErrorMessage = "ReceiveServiceDate is required")]
        public DateOnly? ReceiveServiceDate { get; set; }
    }
}
