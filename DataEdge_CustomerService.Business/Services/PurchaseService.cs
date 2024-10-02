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
using DataEdge_CustomerService.Business.Models.Response.Purchase;
using DataEdge_CustomerService.Business.Models.Request.Purchase;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DataEdge_CustomerService.Business.Services
{

}
public class PurchaseService : IPurchaseService
{
    private readonly DataContext _dbContext;

    public PurchaseService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }


    /// <summary>
    /// Read by id
    /// </summary>
    public async Task<ReadPurchaseByIdResponse> ReadById(int id)
    {
        try
        {


            var entity = await _dbContext.Purchases
    .Include(p => p.Shop)
    .FirstOrDefaultAsync(p => p.Id == id);

            if (entity is null)
            {
                return new ReadPurchaseByIdResponse
                {
                    ErrorMessage = "A keresett elem nem található!"
                };
            }
            else
            {
                return new ReadPurchaseByIdResponse
                {
                    Result = new PurchaseModel
                    {
                        Id = entity.Id,
                        Date = entity.Date,
                        PurchaseAmount = entity.PurchaseAmount,
                        CashRegisterId = entity.CashRegisterId,
                        PartnerId = entity.PartnerId,
                        ShopName = entity?.Shop?.Name
                    }
                };
            }


        }
        catch (Exception ex)
        {
            return new ReadPurchaseByIdResponse
            {
                ErrorMessage = ex.Message
            };
        }
    }

    /// <summary>
    /// List
    /// </summary>
    public async Task<ListPurchaseResponse> List(ListPurchaseRequest request)
    {
        try
        {
      

            IQueryable <Purchase> query = _dbContext.Purchases.Include(p => p.Shop).Include(p => p.Shop);

            if (request.Id.HasValue && request.Id.Value != 0)
            {
                query = query.Where(x => x.Id.ToString().Contains(request.Id.Value.ToString()));
            }

            if (request.PurchaseAmount.HasValue && request.PurchaseAmount.Value != 0)
            {
                query = query.Where(x => x.PurchaseAmount.ToString().Contains(request.PurchaseAmount.Value.ToString()));
            }

            if (request.CashRegisterId.HasValue && request.CashRegisterId.Value != 0)
            {
                query = query.Where(x => x.CashRegisterId.ToString().Contains(request.CashRegisterId.Value.ToString()));
            }

            if (request.PartnerId.HasValue && request.PartnerId.Value != 0)
            {
                query = query.Where(x => x.PartnerId.ToString().Contains(request.PartnerId.Value.ToString()));
            }

            if (request.Date != DateTime.MinValue)
            {
    
                query = query.Where(x => x.Date.Year == request.Date.Year &&
                             x.Date.Month == request.Date.Month &&
                             x.Date.Day == request.Date.Day);
            }

            if (!string.IsNullOrWhiteSpace(request.ShopName))
            {
                query = query.Where(x => x.Shop.Name.ToLower().Contains(request.ShopName.ToLower()));
            }



            var entities = await query.ToListAsync();

            return new ListPurchaseResponse
            {
                Result = entities.Select(x => new PurchaseModel
                {
                    Id = x.Id,
                    Date = x.Date.ToUniversalTime(),
                    PurchaseAmount = x.PurchaseAmount,
                    CashRegisterId = x.CashRegisterId,
                    PartnerId = x.PartnerId,
                    ShopName = x?.Shop?.Name
                }).ToList()
            };
        }
        catch (Exception ex)
        {
            return new ListPurchaseResponse
            {
                ErrorMessage = ex.Message
            };
        }

    }

    /// <summary>
    /// Create
    /// </summary>
    public async Task<CreatePurchaseResponse> Create(CreatePurchaseRequest request)
    {
        var response = new CreatePurchaseResponse();

        try
        {
            if (request is null)
                response.ErrorMessage = "Hibás kérés objektum!";

            if (request.CashRegisterId is null)
                response.ErrorMessage = "Pénztárgép megadása kötelező!";

            if (request.PurchaseAmount is null)
                response.ErrorMessage = "Vásárlási összeg megadása kötelező!";

            if (request.PartnerId is null)
                response.ErrorMessage = "Partner azonosító megadása kötelező!";



            if (String.IsNullOrEmpty(response.ErrorMessage))
            {
                var entity = new Purchase
                {

                    Date = DateTime.UtcNow,
                    PurchaseAmount = request.PurchaseAmount.Value,
                    CashRegisterId = request.CashRegisterId.Value,
                    PartnerId = request.PartnerId.Value,
                    ShopId = 2011076
                };
                await _dbContext.Purchases.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                response.Result = new PurchaseModel
                {
                    Id = entity.Id,
                    Date = entity.Date,
                    PurchaseAmount = entity.PurchaseAmount,
                    CashRegisterId = entity.CashRegisterId,
                    PartnerId = entity.PartnerId,
                    ShopName = entity?.Shop?.Name
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
    public async Task<UpdatePurchaseResponse> Update(UpdatePurchaseRequest request)
    {

        var response = new UpdatePurchaseResponse();

        try
        {
            if (request is null)
                response.ErrorMessage = "Hibás kérés objektum!";

            if (request.CashRegisterId is null)
                response.ErrorMessage = "Pénztárgép megadása kötelező!";

            if (request.PurchaseAmount is null)
                response.ErrorMessage = "Vásárlási összeg megadása kötelező!";

            if (request.PartnerId is null)
                response.ErrorMessage = "Partner azonosító megadása kötelező!";



            if (String.IsNullOrEmpty(response.ErrorMessage))
            {
                var entity = await _dbContext.Purchases
                .Include(p => p.Shop)
                .FirstOrDefaultAsync(p => p.Id == request.Id);

                entity.Date = request.Date;
                entity.PurchaseAmount = request.PurchaseAmount ?? 0;
                entity.CashRegisterId = request.CashRegisterId ?? 0;
                entity.PartnerId = request.PartnerId ?? 0;
                _dbContext.Purchases.Update(entity);

                await _dbContext.SaveChangesAsync();
                response.Result = new PurchaseModel
                {
                    Id = entity.Id,
                    Date = entity.Date,
                    PurchaseAmount = entity.PurchaseAmount,
                    CashRegisterId = entity.CashRegisterId,
                    PartnerId = entity.PartnerId,
                    ShopName = entity?.Shop?.Name

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
