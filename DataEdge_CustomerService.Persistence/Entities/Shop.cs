using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Persistence.Entities
{
    public class Shop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PartnerID { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
    }
}
