using DataEdge_CustomerService.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Business.Models.DTO
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string ArticleNumber { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public string QuantitativeUnit { get; set; }
        public float NetPrice { get; set; }
        public int Version { get; set; }
        public int PartnerId { get; set; }
    }
}
