using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Persistence.Entities
{
    public class Purchase
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float PurchaseAmount { get; set; }
        public int CashRegisterId { get; set; }
        public int PartnerId { get; set; }
        public int ShopId { get; set; }
        public ICollection<PurchaseItem> PurchaseItems { get; set; }
        public Shop Shop { get; set; }
    }
}
