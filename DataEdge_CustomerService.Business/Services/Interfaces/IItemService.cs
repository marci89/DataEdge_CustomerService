using DataEdge_CustomerService.Business.Models.Request.Item;
using DataEdge_CustomerService.Business.Models.Request.Shop;
using DataEdge_CustomerService.Business.Models.Response.Item;
using DataEdge_CustomerService.Business.Models.Response.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Business.Services.Interfaces
{

    public interface IItemService
    {
        Task<ReadItemByIdResponse> ReadById(int id);
        Task<ListItemResponse> List(ListItemRequest request);
        Task<CreateItemResponse> Create(CreateItemRequest request);
        Task<UpdateItemResponse> Update(UpdateItemRequest request);
    }
}