using System;
using System.ComponentModel.DataAnnotations;
using KTH.MODELS.Helper;

namespace KTH.MODELS.Custom.Request.TransOrder
{
	public class CreateTransOrderItemRequest
	{
		public CreateTransOrderItemRequest()
		{
		}

        /// <summary>
        /// รายการสินค้า
        /// </summary>
        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string MasterItemOrderId { get; set; } = null!;

        /// <summary>
        /// สปสช จ่าย
        /// </summary>
        public decimal NhsoPaid { get; set; }

        /// <summary>
        /// ส่วนลดพิเศษ
        /// </summary>
        public decimal SpecialDiscountPaid { get; set; }

        /// <summary>
        /// เงินสงเคราะห์
        /// </summary>
        public decimal AidPaid { get; set; }

        /// <summary>
        /// 1663 จ่าย
        /// </summary>
        public decimal X1663Paid { get; set; }

        /// <summary>
        /// เงินสำรองจ่าย
        /// </summary>
        public decimal Reserve { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }


    }
}

