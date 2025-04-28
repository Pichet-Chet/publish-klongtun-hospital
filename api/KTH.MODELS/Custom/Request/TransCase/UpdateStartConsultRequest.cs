using System;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.TransCase
{
	public class UpdateStartConsultRequest
	{
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; }

        public string? StartConsultRemark { get; set; }
    }
}

