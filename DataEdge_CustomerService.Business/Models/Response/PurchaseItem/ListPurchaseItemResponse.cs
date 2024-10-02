using DataEdge_CustomerService.Business.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Business.Models.Response.PurchaseItem
{
    public class ListPurchaseItemResponse : ResponseBase
    {
        public List<PurchaseItemModel> Result { get; set; }
    }
}
