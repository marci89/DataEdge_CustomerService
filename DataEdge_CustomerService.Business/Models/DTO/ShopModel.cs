using DataEdge_CustomerService.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Business.Models.DTO
{
    public class ShopModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PartnerId { get; set; }
    }
}
