using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Persistence.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string ArticleNumber { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string QuantitativeUnit { get; set; }
        public float NetPrice { get; set; }
        public int Version { get; set; }
        public int PartnerId { get; set; }
        public PurchaseItem PurchaseItem { get; set; }
    }
}
