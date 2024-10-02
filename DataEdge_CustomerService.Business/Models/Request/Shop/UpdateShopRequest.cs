using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Business.Models.Request.Shop
{
    public class UpdateShopRequest : CreateShopRequest
    {
        public int Id { get; set; }
    }
}
