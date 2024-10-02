using DataEdge_CustomerService.Business.Services.Interfaces;
using DataEdge_CustomerService.Persistence;
using DataEdge_CustomerService.Persistence.Entities;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Business.Services
{


    public class DataBaseImportService : IDataBaseImportService
    {
        private readonly DataContext _dbContext;
        private HashSet<int> NonexistingPurchaseIds = new HashSet<int>();

        public DataBaseImportService(DataContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<string> Execute()
        {

            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources/feladat_adat_20200617.xlsx");
            var isSuccess = true;

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var purchaseSheet = package.Workbook.Worksheets["vasarlas"];
                var itemSheet = package.Workbook.Worksheets["cikkek"];
                var purchaseItemSheet = package.Workbook.Worksheets["vasarlas_tetel"];
                var shopSheet = package.Workbook.Worksheets["bolt"];

                var shops = GetShopsFromSheet(shopSheet);
                var purchases = GetPurchasesFromSheet(purchaseSheet);
                var items = GetItemsFromSheet(itemSheet);
                var purchaseItems = GetPurchaseItemsFromSheet(purchaseItemSheet, purchases);

                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _dbContext.Shops.AddRange(shops);
                        await _dbContext.SaveChangesAsync();

                        _dbContext.Purchases.AddRange(purchases);
                        await _dbContext.SaveChangesAsync();

                        _dbContext.Items.AddRange(items);
                        await _dbContext.SaveChangesAsync();


                        var validPurchaseIds = _dbContext.Purchases.Select(p => p.Id).ToHashSet();
                        var purchaseItemsToInsert = purchaseItems
                            .Where(pi => validPurchaseIds.Contains(pi.PurchaseID))
                            .ToList();

                        _dbContext.PurchaseItems.AddRange(purchaseItemsToInsert);
                        await _dbContext.SaveChangesAsync();

                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        isSuccess = false;
                        throw;
                    }
                }

                    return isSuccess ? "Migration was success" : "Migration was unsuccess";

            }
        }

        #region private methods

        private List<Shop> GetShopsFromSheet(ExcelWorksheet sheet)
        {
            var shops = new List<Shop>();
            for (int row = 2; row <= sheet.Dimension.End.Row; row++)
            {
                var shop = new Shop
                {
                    Id = int.Parse(sheet.Cells[row, 1].Text),
                    Name = sheet.Cells[row, 2].Text,
                    PartnerID = int.Parse(sheet.Cells[row, 3].Text),

                };
                shops.Add(shop);
            }
            return shops;
        }

        private List<Purchase> GetPurchasesFromSheet(ExcelWorksheet sheet)
        {
            var purchases = new List<Purchase>();
            var existingIds = new HashSet<int>();


            for (int row = 2; row <= sheet.Dimension.End.Row; row++)
            {
                var id = int.Parse(sheet.Cells[row, 1].Text);


                if (!existingIds.Contains(id))
                {

                    var purchase = new Purchase
                    {
                        Id = int.Parse(sheet.Cells[row, 1].Text),
                        Date = DateTime.Parse(sheet.Cells[row, 2].Text).ToUniversalTime(),
                        PurchaseAmount = float.Parse(sheet.Cells[row, 3].Text),
                        CashRegisterId = int.Parse(sheet.Cells[row, 4].Text),
                        PartnerId = int.Parse(sheet.Cells[row, 5].Text),
                        ShopId = int.Parse(sheet.Cells[row, 6].Text)
                    };

                    purchases.Add(purchase);
                    existingIds.Add(id);
                }
                else NonexistingPurchaseIds.Add(id);
            }
            return purchases;
        }

        private List<Item> GetItemsFromSheet(ExcelWorksheet sheet)
        {
            var items = new List<Item>();
            for (int row = 2; row <= sheet.Dimension.End.Row; row++)
            {

                var id = int.Parse(sheet.Cells[row, 1].Text);
                if (!NonexistingPurchaseIds.Contains(id))
                {

                    var item = new Item
                    {
                        Id = int.Parse(sheet.Cells[row, 1].Text),
                        ArticleNumber = sheet.Cells[row, 2].Text,
                        Barcode = sheet.Cells[row, 3].Text,
                        Name = sheet.Cells[row, 4].Text,
                        QuantitativeUnit = sheet.Cells[row, 5].Text,
                        Version = int.Parse(sheet.Cells[row, 7].Text),
                        PartnerId = int.Parse(sheet.Cells[row, 8].Text)
                    };

                    if (float.TryParse(sheet.Cells[row, 6].Text, NumberStyles.Float, CultureInfo.InvariantCulture, out float netPrice))
                    {
                        item.NetPrice = netPrice;
                    }
                    else
                    {
                        item.NetPrice = 0.0f;
                    }


                    items.Add(item);
                }
            }
            return items;
        }

        private List<PurchaseItem> GetPurchaseItemsFromSheet(ExcelWorksheet sheet, List<Purchase> purchases)
        {
            var purchaseItems = new List<PurchaseItem>();
            var existingIds = new HashSet<int>();

            for (int row = 2; row <= sheet.Dimension.End.Row; row++)
            {
                var id = int.Parse(sheet.Cells[row, 1].Text);
              //  var isParentExists = purchases.Select(x => (x.Id == int.Parse(sheet.Cells[row, 3].Text))).Any();
              


                if (!existingIds.Contains(id))
                {

                    var purchaseItem = new PurchaseItem
                    {
                        Id = int.Parse(sheet.Cells[row, 1].Text),
                        PartnerCtID = int.Parse(sheet.Cells[row, 2].Text),
                        PurchaseID = int.Parse(sheet.Cells[row, 3].Text),
                        PartnerID = int.Parse(sheet.Cells[row, 6].Text)
                    };

                    if (float.TryParse(sheet.Cells[row, 4].Text, NumberStyles.Float, CultureInfo.InvariantCulture, out float quantity))
                    {
                        purchaseItem.Quantity = quantity;
                    }
                    else
                    {
                        purchaseItem.Quantity = 0.0f;
                    }

                    if (float.TryParse(sheet.Cells[row, 5].Text, NumberStyles.Float, CultureInfo.InvariantCulture, out float gross))
                    {
                        purchaseItem.Gross = gross;
                    }
                    else
                    {
                        purchaseItem.Gross = 0.0f;
                    }



                    purchaseItems.Add(purchaseItem);
                   existingIds.Add(id);
                }
            }
            return purchaseItems;
        }

        #endregion
    }
}




