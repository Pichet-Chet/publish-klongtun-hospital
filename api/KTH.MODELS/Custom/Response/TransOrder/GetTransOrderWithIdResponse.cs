using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Response.TransOrder
{
    public class GetTransOrderWithIdResponse
    {
        public GetTransOrderWithIdResponseData Data { get; set; }
        public MessageAlertResponse MessageAlert { get; set; }
    }
    public class GetTransOrderWithIdResponseData
    {
        public Guid Id { get; set; }
        public Guid TransCaseId { get; set; }

        public string? RemarkOrder { get; set; }

        public string? RemarkSpecialDiscountRequest { get; set; }

        public string? RemarkSpecialDiscountApprove { get; set; }

        public string MasterStatusCode { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string CreatedBySysRoleCode { get; set; } 
        public string OrderNo { get; set; }
        public Guid? StaffApproveId { get; set; }

        public List<GetTransOrderWithIdResponseDataItem> OrderItem { get; set; }

        public List<GetTransOrderWithIdTransPaymentHeaderList> PaymentHeaderList { get; set; }

        public string MasterStatusName { get; set; }
    }

    public class GetTransOrderWithIdResponseDataItem
    {
        public Guid Id { get; set; }
        public Guid TransOrderId { get; set; }
        public Guid MasterItemOrderId { get; set; }
        public decimal NhsoPaid { get; set; }
        public decimal SpecialDiscountPaid { get; set; }
        public decimal AidPaid { get; set; }
        public decimal X1663Paid { get; set; }
        public decimal Reserve { get; set; }
        public decimal Remain { get; set; }

        public bool IsActive { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool Paid { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

   

    public class GetTransOrderWithIdTransPaymentHeaderList
    {
        public Guid Id { get; set; }

        public string TransactionNo { get; set; } = null!;

        public DateTime TransactionDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string? Remark { get; set; }

        public bool IsActive { get; set; }

        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedDate { get; set; }

        public string UpdatedBy { get; set; } = null!;

        public DateTime UpdatedDate { get; set; }

        public bool IsReceipt { get; set; }

        public Guid? TransOrderId { get; set; }

        public string MasterStatusCode { get; set; } = null!;

        public Guid TransClientId { get; set; }

        public short MoneyBucket { get; set; }

        public Guid TransCaseId { get; set; }

        public string TransCaseNo { get; set; } = null!;

        public decimal PaymentCash { get; set; }

        public decimal PaymentQrCode { get; set; }

        public decimal PaymentCredit { get; set; }

        public string? PaymentCreditCard { get; set; }

        public string TypePaymet { get; set; } = null!;

        public bool IsCloseBalance { get; set; }
    }
}
