using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Business.Models.Request.Purchase
{
    public class UpdatePurchaseRequest
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float? PurchaseAmount { get; set; }
        public int? CashRegisterId { get; set; }
        public int? PartnerId { get; set; }
    }
}
