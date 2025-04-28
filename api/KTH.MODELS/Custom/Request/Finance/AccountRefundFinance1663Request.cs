using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.MODELS.Custom.Request.Finance
{
    public class AccountRefundFinance1663Request: FilterModel
    {
        [Required(ErrorMessage =("Month is required"))]
        public int Month { get; set; }
        [Required(ErrorMessage = ("Year is required"))]
        public int Year { get; set; }
    }
}
