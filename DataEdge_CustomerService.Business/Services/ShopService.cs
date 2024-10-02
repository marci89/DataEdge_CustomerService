using DataEdge_CustomerService.Business.Models.DTO;
using DataEdge_CustomerService.Business.Models.Request.Shop;
using DataEdge_CustomerService.Business.Models.Response.Shop;
using DataEdge_CustomerService.Business.Services.Interfaces;
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


    public class ShopService : IShopService
    {
        private readonly DataContext _dbContext;

        public ShopService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Read by id
        /// </summary>
        public async Task<ReadShopByIdResponse> ReadById(int id)
        {
            try
            {
                var entity = await _dbContext.Shops.FindAsync(id);

                if (entity is null)
                {
                    return new ReadShopByIdResponse
                    {
                        ErrorMessage = "A keresett elem nem található!"
                    };
                } else
                {
                    return new ReadShopByIdResponse
                    {
                        Result = new ShopModel { Id = entity.Id, Name = entity.Name, PartnerId = entity.PartnerID }
                    };
                }

             
            }
            catch (Exception ex)
            {
                return new ReadShopByIdResponse
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// List
        /// </summary>
        public async Task<ListShopResponse> List(ListShopRequest request)
        {
            try
            {
                IQueryable<Shop> query = _dbContext.Shops;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    query = query.Where(x => x.Id.ToString().Contains(request.Id.Value.ToString()));
                }

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
                }

                if (request.PartnerID.HasValue && request.PartnerID.Value != 0)
                {
                   // query = query.Where(shop => shop.PartnerID == request.PartnerID.Value);
                    query = query.Where(x => x.PartnerID.ToString().Contains(request.PartnerID.Value.ToString()));
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
        /// Create
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



                if (String.IsNullOrEmpty(response.ErrorMessage))
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

        /// <summary>
        /// Update 
        /// </summary>
        public async Task<UpdateShopResponse> Update(UpdateShopRequest request)
        {

            var response = new UpdateShopResponse();

            try
            {
                if (request is null)
                    response.ErrorMessage = "Hibás kérés objektum!";

                if (String.IsNullOrWhiteSpace(request.Name))
                    response.ErrorMessage = "Név megadása kötelező!";

                if (request.PartnerID is null)
                    response.ErrorMessage = "Partner azonosító megadása kötelező!";



                if (String.IsNullOrEmpty(response.ErrorMessage))
                {
                    var entity = await _dbContext.Shops.FindAsync(request.Id);
                    entity.Name = request.Name;
                    entity.PartnerID = request.PartnerID ?? 0;
                    _dbContext.Shops.Update(entity);
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