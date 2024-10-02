using DataEdge_CustomerService.Business.Models.DTO;
using DataEdge_CustomerService.Business.Models.Request.Shop;
using DataEdge_CustomerService.Business.Models.Response.Shop;
using DataEdge_CustomerService.Business.Services.Interfaces;
using DataEdge_CustomerService.Persistence.Entities;
using DataEdge_CustomerService.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataEdge_CustomerService.Business.Models.Response.PurchaseItem;
using DataEdge_CustomerService.Business.Models.Request.PurchaseItem;
using System.Text.RegularExpressions;

namespace DataEdge_CustomerService.Business.Services
{

    public class PurchaseItemService : IPurchaseItemService
    {
        private readonly DataContext _dbContext;

        public PurchaseItemService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Read by id
        /// </summary>
        public async Task<ReadPurchaseItemByIdResponse> ReadById(int id)
        {
            try
            {
                var entity = await _dbContext.PurchaseItems.FindAsync(id);

                if (entity is null)
                {
                    return new ReadPurchaseItemByIdResponse
                    {
                        ErrorMessage = "A keresett elem nem található!"
                    };
                }
                else
                {
                    return new ReadPurchaseItemByIdResponse
                    {
                        Result = new PurchaseItemModel
                        {
                            Id = entity.Id,
                            PartnerCtID = entity.PartnerCtID,
                            Quantity = entity.Quantity,
                            Gross = entity.Gross,
                            PartnerID = entity.PartnerID
                        }
                    };
                }


            }
            catch (Exception ex)
            {
                return new ReadPurchaseItemByIdResponse
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// List
        /// </summary>
        public async Task<ListPurchaseItemResponse> List(ListPurchaseItemRequest request)
        {


            try
            {
                IQueryable<PurchaseItem> query = _dbContext.PurchaseItems.Include(p => p.Item);


                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    query = query.Where(x => x.Id.ToString().Contains(request.Id.Value.ToString()));
                }

                if (request.PartnerCtID.HasValue && request.PartnerCtID.Value != 0)
                {
                    query = query.Where(x => x.PartnerCtID.ToString().Contains(request.PartnerCtID.Value.ToString()));
                }

                if (request.Quantity.HasValue && request.Quantity.Value != 0)
                {
                    query = query.Where(x => x.Quantity.ToString().Contains(request.Quantity.Value.ToString()));
                }

                if (request.Gross.HasValue && request.Gross.Value != 0)
                {
                    query = query.Where(x => x.Gross.ToString().Contains(request.Gross.Value.ToString()));
                }



                if (request.PartnerID.HasValue && request.Id.Value != 0)
                {
                    query = query.Where(x => x.PartnerID.ToString().Contains(request.PartnerID.Value.ToString()));
                }

                var entities = await query.Where(x => x.PurchaseID == request.PurchaseID).ToListAsync();

                return new ListPurchaseItemResponse
                {
                    Result = entities.Select(x => new PurchaseItemModel
                    {
                        Id = x.Id,
                        PartnerCtID = x.PartnerCtID,
                        PurchaseID = request.PurchaseID.Value,
                        Quantity = x.Quantity,
                        Gross = x.Gross,
                        PartnerID = x.PartnerID
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                return new ListPurchaseItemResponse
                {
                    ErrorMessage = ex.Message
                };
            }

        }

        /// <summary>
        /// Create
        /// </summary>
        public async Task<CreatePurchaseItemResponse> Create(CreatePurchaseItemRequest request)
        {
            var response = new CreatePurchaseItemResponse();

            try
            {
                if (request is null)
                    response.ErrorMessage = "Hibás kérés objektum!";


                if (request.PartnerCtID is null)
                    response.ErrorMessage = "Cikk megadása kötelező!";

                if (request.Quantity is null)
                    response.ErrorMessage = "Mennyiség megadása kötelező!";

                if (request.Gross is null)
                    response.ErrorMessage = "Bruttó ár megadása kötelező!";

                if (request.PartnerID is null)
                    response.ErrorMessage = "Partner azonosító megadása kötelező!";



                if (String.IsNullOrEmpty(response.ErrorMessage))
                {
                    var entity = new PurchaseItem
                    {
                        PartnerCtID = request.PartnerCtID,
                        Quantity = request.Quantity.Value,
                        Gross = request.Gross.Value,
                        PartnerID = request.PartnerID.Value,
                        PurchaseID = request.PurchaseID.Value
                    };
                    await _dbContext.PurchaseItems.AddAsync(entity);
                    await _dbContext.SaveChangesAsync();
                    response.Result = new PurchaseItemModel
                    {
                        Id = entity.Id,
                        PartnerCtID = entity.PartnerCtID,
                        Quantity = entity.Quantity,
                        Gross = entity.Gross,
                        PartnerID = entity.PartnerID,
                        PurchaseID = entity.PurchaseID,
                    };
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
        public async Task<UpdatePurchaseItemResponse> Update(UpdatePurchaseItemRequest request)
        {

            var response = new UpdatePurchaseItemResponse();

            try
            {

                if (request is null)
                    response.ErrorMessage = "Hibás kérés objektum!";


                if (request.PartnerCtID is null)
                    response.ErrorMessage = "Cikk megadása kötelező!";

                if (request.Quantity is null)
                    response.ErrorMessage = "Mennyiség megadása kötelező!";

                if (request.Gross is null)
                    response.ErrorMessage = "Bruttó ár megadása kötelező!";

                if (request.PartnerID is null)
                    response.ErrorMessage = "Partner azonosító megadása kötelező!";



                if (String.IsNullOrEmpty(response.ErrorMessage))
                {
                    var entity = await _dbContext.PurchaseItems.FindAsync(request.Id);
                    entity.PartnerCtID = request.PartnerCtID;
                    entity.Quantity = request.Quantity.Value;
                    entity.Gross = request.Gross.Value;
                    entity.PartnerID = request.PartnerID ?? 0;
                    _dbContext.PurchaseItems.Update(entity);
                    await _dbContext.SaveChangesAsync();
                    response.Result = new PurchaseItemModel
                    {
                        Id = entity.Id,
                        PartnerCtID = entity.PartnerCtID,
                        Quantity = entity.Quantity,
                        Gross = entity.Gross,
                        PartnerID = entity.PartnerID
                    };
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