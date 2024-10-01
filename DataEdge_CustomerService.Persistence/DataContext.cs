using DataEdge_CustomerService.Persistence.Configurations;
using DataEdge_CustomerService.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Persistence
{
    /// <summary>
    /// Database context
    /// </summary>
    public class DataContext : DbContext
    {
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }
        public DbSet<Item> Items { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set the tables with the current table's configuratuion class. This line handle all of tables where exists the table name.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShopConfiguration).Assembly);
        }
    }
}
