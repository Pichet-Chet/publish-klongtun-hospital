using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KTH.MODELS.DocumentBuilder;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.TransOrder
{
    public class CreateTransOrderFreeRequest
    {
        public CreateTransOrderFreeRequest()
        {
            Items = new List<CreateTransOrderItemRequest>();
        }

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string TransCaseId { get; set; } = null!;

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string CreatedBy { get; set; } = null!;

        [Required(ErrorMessage = "CreatedBySysRoleCode is required")]
        public string CreatedBySysRoleCode { get; set; }

        public List<CreateTransOrderItemRequest> Items { get; set; }
    }
}
