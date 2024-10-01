using DataEdge_CustomerService.Business.Models.DTO;
using DataEdge_CustomerService.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Business.Models.Response
{

    public class CreateShopResponse : ResponseBase
    {
        public ShopModel Result { get; set; }
    }
}