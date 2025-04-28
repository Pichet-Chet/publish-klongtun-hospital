using System;
using System.ComponentModel.DataAnnotations;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.TransCase
{
	public class UpdateAssignConsultRoomUsRequest
	{
		public UpdateAssignConsultRoomUsRequest()
		{
		}

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string CaseId { get; set; } = null!;

		public string UsRemark { get; set; } = null!;

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string UsBy { get; set; } = null!;

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string MasterGestationalAgeId { get; set; } = null!;

    }


    public class UpdateAssignConsultRoomPtRequest
    {
        public UpdateAssignConsultRoomPtRequest()
        {
        }

        public string CaseId { get; set; } = null!;

        public bool IsPtChecked { get; set; }

        [Required]
        public string PtPosComment { get; set; }

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string UpdatedBy { get; set; } = null!;


    }
}

