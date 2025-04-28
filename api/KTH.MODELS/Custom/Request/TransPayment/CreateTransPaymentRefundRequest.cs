using KTH.MODELS.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.TransPayment
{
    public class CreateTransPaymentRefundRequest
    {

        public decimal TotalAmount { get; set; }

        public string? Remark { get; set; }

        public string CreatedBy { get; set; } = null!;

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string TransClientId { get; set; }

        [ValidGuid(ErrorMessage = "Invalid Guid format")]
        public string TransCaseId { get; set; }

    }
}
