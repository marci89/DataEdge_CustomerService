using DataEdge_CustomerService.Business.Models.Request.Shop;
using DataEdge_CustomerService.Business.Models.Response.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Business.Services.Interfaces
{
    public interface IShopService
    {
        Task<ReadShopByIdResponse> ReadById(int id);
        Task<ListShopResponse> List(ListShopRequest request);
        Task<CreateShopResponse> Create(CreateShopRequest request);
        Task<UpdateShopResponse> Update(UpdateShopRequest request);
    }
}
