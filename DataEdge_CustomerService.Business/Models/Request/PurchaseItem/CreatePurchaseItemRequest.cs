using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Business.Models.Request.PurchaseItem
{
    public class CreatePurchaseItemRequest
    {
        public int? PartnerCtID { get; set; }
        public int? PurchaseID { get; set; }
        public float? Quantity { get; set; }
        public float? Gross { get; set; }
        public int? PartnerID { get; set; }
    }
}
