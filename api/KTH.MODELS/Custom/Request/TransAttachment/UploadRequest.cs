using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTH.MODELS.Helper;
using Microsoft.AspNetCore.Http;

namespace KTH.MODELS.Custom.Request.TransAttachment
{
    public class UploadRequest
    {
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string ReferanceId {  get; set; }

        [Required(ErrorMessage = "SysType is required")]
        public string SysType {  get; set; }

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string CreatedBy { get; set; }

        public IFormFile File { get; set; }
    }
}
