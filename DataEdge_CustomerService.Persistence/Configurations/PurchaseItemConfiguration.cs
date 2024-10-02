using DataEdge_CustomerService.Persistence.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEdge_CustomerService.Persistence.Configurations
{


public class PurchaseItemConfiguration : IEntityTypeConfiguration<PurchaseItem>
{
    public void Configure(EntityTypeBuilder<PurchaseItem> builder)
    {
        builder.ToTable("PurchaseItem");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
         .ValueGeneratedOnAdd()
         .IsRequired();

        builder.Property(u => u.PartnerCtID)
          .IsRequired(false);

        builder.Property(u => u.PurchaseID)
            .IsRequired();

        builder.Property(u => u.Quantity)
          .IsRequired();


        builder.Property(u => u.Gross)
          .IsRequired();


        builder.Property(u => u.PartnerID)
          .IsRequired();

        builder.HasOne(x => x.Purchase)
     .WithMany(x => x.PurchaseItems)
     .HasForeignKey(c => c.PurchaseID)
     .HasConstraintName("FK_Purchase_PurchaseID")
     .OnDelete(DeleteBehavior.NoAction);

 

            builder.HasOne(p => p.Item)
    .WithMany(i => i.PurchaseItems) 
    .HasForeignKey(p => p.PartnerCtID)
     .HasConstraintName("FK_Item_ItemID")
    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
