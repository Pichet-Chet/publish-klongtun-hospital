using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.Finance
{
    public class PaymentAndRefundRequest : FilterModel
    {
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public string? System {  get; set; }
        public string? Type {  get; set; }
        public string? Status {  get; set; }
    }
}
