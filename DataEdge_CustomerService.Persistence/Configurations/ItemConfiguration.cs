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


    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Item");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(i => i.ArticleNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.Barcode)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(i => i.QuantitativeUnit)
                .IsRequired();

            builder.Property(i => i.NetPrice)
                .IsRequired();

            builder.Property(i => i.Version)
                .IsRequired();

            builder.Property(i => i.PartnerId)
                .IsRequired();


            builder.HasOne(i => i.PurchaseItem)
                .WithOne(p => p.Item)
                .HasForeignKey<PurchaseItem>(p => p.PartnerCtID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}