using DataEdge_CustomerService.Business.Models.Request.Purchase;
using DataEdge_CustomerService.Business.Models.Request.Shop;
using DataEdge_CustomerService.Business.Models.Response.Purchase;
using DataEdge_CustomerService.Business.Models.Response.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Business.Services.Interfaces
{

    public interface IPurchaseService
    {
        Task<ReadPurchaseByIdResponse> ReadById(int id);
        Task<ListPurchaseResponse> List(ListPurchaseRequest request);
        Task<CreatePurchaseResponse> Create(CreatePurchaseRequest request);
        Task<UpdatePurchaseResponse> Update(UpdatePurchaseRequest request);
    }
}