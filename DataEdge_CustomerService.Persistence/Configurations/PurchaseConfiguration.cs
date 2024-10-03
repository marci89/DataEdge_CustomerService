using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataEdge_CustomerService.Persistence.Entities;

namespace DataEdge_CustomerService.Persistence.Configurations
{
    public class PurchaseConfiguration : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
             .ValueGeneratedOnAdd()
             .IsRequired();

            builder.Property(u => u.Date)
              .IsRequired();

            builder.Property(u => u.PurchaseAmount)
                .IsRequired();

            builder.Property(u => u.CashRegisterId)
              .IsRequired();


            builder.Property(u => u.PartnerId)
              .IsRequired();


            builder.Property(u => u.ShopId)
              .IsRequired();

            builder.HasOne(x => x.Shop)
         .WithMany(x => x.Purchases)
         .HasForeignKey(c => c.ShopId)
         .HasConstraintName("FK_Shop_ShopID")
         .OnDelete(DeleteBehavior.NoAction);
        }
    }
}