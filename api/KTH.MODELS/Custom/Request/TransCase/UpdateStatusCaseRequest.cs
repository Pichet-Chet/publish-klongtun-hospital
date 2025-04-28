using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransCase
{
    public class UpdateStatusCaseRequest
    {
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; }

        [Required(ErrorMessage = "MasterStatusCode is required")]
        public string MasterStatusCode { get; set; } = null!;
    }
}
