using KTH.MODELS.Helper;
using System.ComponentModel.DataAnnotations;

namespace KTH.MODELS.Custom.Request.TransOrder
{
    public class UpdateTransOrderRequest
    {
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string Id { get; set; }
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string TransCaseId { get; set; }
        public string? RemarkOrder { get; set; }

        public string? RemarkSpecialDiscountRequest { get; set; }

        public string? RemarkSpecialDiscountApprove { get; set; }
        public string MasterStatusCode { get; set; }

        public string? UpdatedBy { get; set; }

        [Required(ErrorMessage = "OrderType is required")]
        public string OrderType { get; set; }

        public List<UpdateTranOrderItemRequest> Items { get; set; }
    }

    public class UpdateTranOrderItemRequest
    {
        public string? Id { get; set; }
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string MasterItemOrderId { get; set; }
        public decimal NhsoPaid { get; set; }
        public decimal SpecialDiscountPaid { get; set; }
        public decimal AidPaid { get; set; }
        public decimal X1663Paid { get; set; }
        public decimal Reserve { get; set; }
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }
    }
}

