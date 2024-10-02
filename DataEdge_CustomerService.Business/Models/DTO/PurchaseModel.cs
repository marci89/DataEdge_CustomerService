using DataEdge_CustomerService.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Business.Models.DTO
{
    public class PurchaseModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float PurchaseAmount { get; set; }
        public int CashRegisterId { get; set; }
        public int PartnerId { get; set; }
        public string ShopName { get; set; }
    }
}
