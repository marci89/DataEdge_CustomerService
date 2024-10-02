using DataEdge_CustomerService.Business.Models.Request.Purchase;
using DataEdge_CustomerService.Business.Models.Request.PurchaseItem;
using DataEdge_CustomerService.Business.Models.Response.Purchase;
using DataEdge_CustomerService.Business.Models.Response.PurchaseItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Business.Services.Interfaces
{
    public interface IPurchaseItemService
    {
        Task<ReadPurchaseItemByIdResponse> ReadById(int id);
        Task<ListPurchaseItemResponse> List(ListPurchaseItemRequest request);
        Task<CreatePurchaseItemResponse> Create(CreatePurchaseItemRequest request);
        Task<UpdatePurchaseItemResponse> Update(UpdatePurchaseItemRequest request);
    }
}
