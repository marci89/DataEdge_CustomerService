﻿using DataEdge_CustomerService.Business.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Business.Models.Response.Shop
{

    public class CreateShopResponse : ResponseBase
    {
        public ShopModel Result { get; set; }
    }
}