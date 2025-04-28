using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransClient
{
    public class UpdateTransClientRequest
    {
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string? CitizenIdentification { get; set; }

        public string? PassportNumber { get; set; }

        public string? Address { get; set; }

        [Required(ErrorMessage = "DateOfBirth is required")]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "TelephoneNumber is required")]
        public string TelephoneNumber { get; set; } = null!;

        public string? MasterNationalityId { get; set; }

        [Required(ErrorMessage = "IsActive is required")]
        public bool IsActive { get; set; } = true;

        public string? UpdatedBy { get; set; }

        [Required(ErrorMessage = "TelephoneCode is required")]
        public string TelephoneCode { get; set; } = null!;

        public string? TelephoneSecond { get; set; }

        public string? MasterRightTreatmentId { get; set; }

        public string? TranSaleRefCode { get; set; }

        public string? HnNo { get; set; }

        public string? Occupation {  get; set; }

        public string? HostpitalName { get; set; }

    }
}
