using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Business.Models.Request.Purchase
{
    public class CreatePurchaseRequest
    {
        public DateTime Date { get; set; }
        public float? PurchaseAmount { get; set; }
        public int? CashRegisterId { get; set; }
        public int? PartnerId { get; set; }

    }
}
