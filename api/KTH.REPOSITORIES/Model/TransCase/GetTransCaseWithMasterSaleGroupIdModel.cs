using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTH.REPOSITORIES.Model.TransCase
{
    public class GetTransCaseWithMasterSaleGroupIdModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateOnly ReceiveServiceDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Guid? TransSaleId { get; set; }
        public Guid? MasterSaleGroupId { get; set; }
        public List<GetTransCaseWithMasterSaleGroupIdModelOrder> Order { get; set; }
    }

    public class GetTransCaseWithMasterSaleGroupIdModelOrder
    {
        public Guid Id { get; set; }
        public List<GetTransCaseWithMasterSaleGroupIdModelOrderItem> OrderItem { get; set; }
    }

    public class GetTransCaseWithMasterSaleGroupIdModelOrderItem
    {
        public Guid Id { get; set; }
        public Guid MasterItemOrderId { get; set; }
    }
}
