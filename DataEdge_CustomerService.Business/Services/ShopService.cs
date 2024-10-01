using DataEdge_CustomerService.Business.Models;
using DataEdge_CustomerService.Business.Models.DTO;
using DataEdge_CustomerService.Business.Models.Request;
using DataEdge_CustomerService.Business.Models.Response;
using DataEdge_CustomerService.Persistence;
using DataEdge_CustomerService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Business.Services
{


    public class ShopService
    {
        private readonly DataContext _dbContext;

        public ShopService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// List shops
        /// </summary>
        public async Task<ListShopResponse> ListShop(ListShopRequest request)
        {
            try
            {
                IQueryable<Shop> query = _dbContext.Shops;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    query = query.Where(shop => shop.Id.ToString().Contains(request.Id.Value.ToString()));
                }

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    query = query.Where(shop => shop.Name.ToLower().Contains(request.Name.ToLower()));
                }

                if (request.PartnerID.HasValue && request.Id.Value != 0)
                {
                   // query = query.Where(shop => shop.PartnerID == request.PartnerID.Value);
                    query = query.Where(shop => shop.PartnerID.ToString().Contains(request.PartnerID.Value.ToString()));
                }

                var entities = await query.ToListAsync();

                return new ListShopResponse
                {
                    Result = entities.Select(x => new ShopModel { Id = x.Id, Name = x.Name, PartnerId = x.PartnerID }).ToList()
                };
            }
            catch (Exception ex)
            {
                return new ListShopResponse
                {
                    ErrorMessage = ex.Message
                };
            }

        }

        /// <summary>
        /// Create shop
        /// </summary>
        public async Task<CreateShopResponse> Create(CreateShopRequest request)
        {
            var response = new CreateShopResponse();

            try
            {
                if (request is null)
                    response.ErrorMessage = "Hibás kérés objektum!";

                if (String.IsNullOrWhiteSpace(request.Name))
                    response.ErrorMessage = "Név megadása kötelező!";

                if (request.PartnerID is null)
                    response.ErrorMessage = "Partner azonosító megadása kötelező!";



                if (!String.IsNullOrEmpty(response.ErrorMessage))
                {
                    var entity = new Shop { Name = request.Name, PartnerID = request.PartnerID ?? 0 };
                    await _dbContext.Shops.AddAsync(entity);
                    await _dbContext.SaveChangesAsync();
                    response.Result = new ShopModel { Id = entity.Id, Name = entity.Name, PartnerId = entity.PartnerID };
                }

                return response;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = ex.Message;
                return response;
            }
        }
    }
}