using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Business.Models.Request
{
    public class ListShopRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? PartnerID { get; set; }
    }
}
