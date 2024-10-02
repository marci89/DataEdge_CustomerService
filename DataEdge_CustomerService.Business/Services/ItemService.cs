using DataEdge_CustomerService.Business.Models.DTO;
using DataEdge_CustomerService.Business.Services.Interfaces;
using DataEdge_CustomerService.Persistence.Entities;
using DataEdge_CustomerService.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataEdge_CustomerService.Business.Models.Response.Item;
using DataEdge_CustomerService.Business.Models.Request.Item;

namespace DataEdge_CustomerService.Business.Services
{

    public class ItemService : IItemService
    {
        private readonly DataContext _dbContext;

        public ItemService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Read by id
        /// </summary>
        public async Task<ReadItemByIdResponse> ReadById(int id)
        {
            try
            {
                var entity = await _dbContext.Items.FindAsync(id);

                if (entity is null)
                {
                    return new ReadItemByIdResponse
                    {
                        ErrorMessage = "A keresett elem nem található!"
                    };
                }
                else
                {
                    return new ReadItemByIdResponse
                    {
                        Result = new ItemModel
                        {
                            Id = entity.Id,
                            ArticleNumber = entity.ArticleNumber,
                            Barcode = entity.Barcode,
                            Name = entity.Name,
                            QuantitativeUnit = entity.QuantitativeUnit,
                            NetPrice = entity.NetPrice,
                            Version = entity.Version,
                            PartnerId = entity.PartnerId

                        }
                    };
                }


            }
            catch (Exception ex)
            {
                return new ReadItemByIdResponse
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// List
        /// </summary>
        public async Task<ListItemResponse> List(ListItemRequest request)
        {
            try
            {
                IQueryable<Item> query = _dbContext.Items;

                if (request.Id.HasValue && request.Id.Value != 0)
                {
                    query = query.Where(x => x.Id.ToString().Contains(request.Id.Value.ToString()));
                }

                if (!string.IsNullOrWhiteSpace(request.ArticleNumber))
                {
                    query = query.Where(x => x.ArticleNumber.ToLower().Contains(request.ArticleNumber.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(request.Barcode))
                {
                    query = query.Where(x => x.Barcode.ToLower().Contains(request.Barcode.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    query = query.Where(x => x.Name.ToLower().Contains(request.Name.ToLower()));
                }

                if (!string.IsNullOrWhiteSpace(request.QuantitativeUnit))
                {
                    query = query.Where(x => x.QuantitativeUnit.ToLower().Contains(request.QuantitativeUnit.ToLower()));
                }


                if (request.NetPrice.HasValue && request.NetPrice.Value != 0)
                {
                    query = query.Where(x => x.NetPrice.ToString().Contains(request.NetPrice.Value.ToString()));
                }

                if (request.Version.HasValue && request.Version.Value != 0)
                {
                    query = query.Where(x => x.Version.ToString().Contains(request.Version.Value.ToString()));
                }

                if (request.PartnerId.HasValue && request.PartnerId.Value != 0)
                {
                    query = query.Where(x => x.PartnerId.ToString().Contains(request.PartnerId.Value.ToString()));
                }

                var entities = await query.ToListAsync();

                return new ListItemResponse
                {
                    Result = entities.Select(x => new ItemModel
                    {
                        Id = x.Id,
                        ArticleNumber = x.ArticleNumber,
                        Barcode = x.Barcode,
                        Name = x.Name,
                        QuantitativeUnit = x.QuantitativeUnit,
                        NetPrice = x.NetPrice,
                        Version = x.Version,
                        PartnerId = x.PartnerId


                    }).ToList()
                };

            }
            catch (Exception ex)
            {
                return new ListItemResponse
                {
                    ErrorMessage = ex.Message
                };
            }

        }

        /// <summary>
        /// Create
        /// </summary>
        public async Task<CreateItemResponse> Create(CreateItemRequest request)
        {
            var response = new CreateItemResponse();

            try
            {
                if (request is null)
                    response.ErrorMessage = "Hibás kérés objektum!";

                if (String.IsNullOrWhiteSpace(request.ArticleNumber))
                    response.ErrorMessage = "Cikkszám megadása kötelező!";

                if (String.IsNullOrWhiteSpace(request.Barcode))
                    response.ErrorMessage = "Vonalkód megadása kötelező!";

                if (String.IsNullOrWhiteSpace(request.Name))
                    response.ErrorMessage = "Név megadása kötelező!";

                if (String.IsNullOrWhiteSpace(request.QuantitativeUnit))
                    response.ErrorMessage = "Mennyiségi egység megadása kötelező!";

                if (request.Version is null)
                    response.ErrorMessage = "Verzió megadása kötelező!";

                if (request.PartnerId is null)
                    response.ErrorMessage = "Verzió megadása kötelező!";




                if (String.IsNullOrEmpty(response.ErrorMessage))
                {
                    var entity = new Item
                    {
                        ArticleNumber = request.ArticleNumber,
                        Barcode = request.Barcode,
                        Name = request.Name,
                        QuantitativeUnit = request.QuantitativeUnit,
                        NetPrice = request.NetPrice.Value,
                        Version = request.Version.Value,
                        PartnerId = request.PartnerId.Value
                    };

                    await _dbContext.Items.AddAsync(entity);
                    await _dbContext.SaveChangesAsync();
                    response.Result = new ItemModel
                    {
                        ArticleNumber = entity.ArticleNumber,
                        Barcode = entity.Barcode,
                        Name = entity.Name,
                        QuantitativeUnit = entity.QuantitativeUnit,
                        NetPrice = entity.NetPrice,
                        Version = entity.Version,
                        PartnerId = entity.PartnerId

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
        public async Task<UpdateItemResponse> Update(UpdateItemRequest request)
        {

            var response = new UpdateItemResponse();

            try
            {
                if (request is null)
                    response.ErrorMessage = "Hibás kérés objektum!";

                if (String.IsNullOrWhiteSpace(request.ArticleNumber))
                    response.ErrorMessage = "Cikkszám megadása kötelező!";

                if (String.IsNullOrWhiteSpace(request.Barcode))
                    response.ErrorMessage = "Vonalkód megadása kötelező!";

                if (String.IsNullOrWhiteSpace(request.Name))
                    response.ErrorMessage = "Név megadása kötelező!";

                if (String.IsNullOrWhiteSpace(request.QuantitativeUnit))
                    response.ErrorMessage = "Mennyiségi egység megadása kötelező!";

                if (request.Version is null)
                    response.ErrorMessage = "Verzió megadása kötelező!";

                if (request.PartnerId is null)
                    response.ErrorMessage = "Verzió megadása kötelező!";


                if (String.IsNullOrEmpty(response.ErrorMessage))
                {
                    var entity = await _dbContext.Items.FindAsync(request.Id);
                    entity.ArticleNumber = request.ArticleNumber;
                    entity.Barcode = request.Barcode;
                    entity.Name = request.Name;
                    entity.QuantitativeUnit = request.QuantitativeUnit;
                    entity.NetPrice = request.NetPrice.Value;
                    entity.Version = request.Version.Value;
                    entity.PartnerId = request.PartnerId ?? 0;

                    _dbContext.Items.Update(entity);
                    await _dbContext.SaveChangesAsync();

                    response.Result = new ItemModel
                    {
                        Id = entity.Id,
                        ArticleNumber = entity.ArticleNumber,
                        Barcode = entity.Barcode,
                        Name = entity.Name,
                        QuantitativeUnit = entity.QuantitativeUnit,
                        NetPrice = entity.NetPrice,
                        Version = entity.Version,
                        PartnerId = entity.PartnerId
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