using KTH.MODELS.Constants;
using KTH.MODELS.Helper;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static KTH.MODELS.Constants.ConstantsMassage;

namespace KTH.MODELS.Custom.Request.TransOrder
{
    public class CreateTransOrderRequest
    {
        public CreateTransOrderRequest()
        {
            Items = new List<CreateTransOrderItemRequest>();
        }

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string TransCaseId { get; set; } = null!;

        public string? RemarkOrder { get; set; }

        public string? RemarkSpecialDiscountRequest { get; set; }

        public string? RemarkSpecialDiscountApprove { get; set; }

        [Required(ErrorMessage = "MasterStatusCode is required")]
        public string MasterStatusCode { get; set; }

        public string CreatedBy { get; set; } = null!;

        [Required(ErrorMessage = "CreatedBySysRoleCode is required")]
        public string CreatedBySysRoleCode { get; set; }

        [Required(ErrorMessage = "OrderType is required")]
        public string OrderType { get; set; }

        public List<CreateTransOrderItemRequest> Items { get; set; }
    }
}

