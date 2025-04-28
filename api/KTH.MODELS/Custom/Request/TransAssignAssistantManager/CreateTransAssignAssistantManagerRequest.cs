using System;
using System.ComponentModel.DataAnnotations;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.MasterSaleGroup
{
    public class CreateTransAssignAssistantManagerRequest
    {

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string TransStaffId { get; set; } = null!;

        public string StaffName { get; set; } = null!;

        public string? Reason { get; set; }

        public string Password { get; set; } = null!;

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }
    }
}

