using System;
using System.ComponentModel.DataAnnotations;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.TransCase
{
	public class CreateNewAppointmentTransCaseRequest
	{
		public string OldCaseId { get; set; } = null!; // เคสนัดหมายเก่า

        [Required(ErrorMessage = "New appointment is required")]
        public DateOnly? NewAppointment { get; set; } // วันที่นัดหมายใหม่

        public bool IsFreeUs { get; set; } // ฟรี US

        public bool IsFreePt { get; set; } // ฟรี PT

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string MasterReasonNewAppointmentId { get; set; } = null!; // หมวดหมู่เหตุผลนัดหมายใหม่

        public string ReasonNewAppointment { get; set; } = null!; // หมายเหตุนัดหมายใหม่

        public string CreatedBy { get; set; } = null!;

    }
}

