using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataEdge_CustomerService.Persistence.Entities;

namespace DataEdge_CustomerService.Persistence.Configurations
{

    public class ShopConfiguration : IEntityTypeConfiguration<Shop>
    {

        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.ToTable("Shop");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                  .ValueGeneratedOnAdd()
                  .IsRequired();

            builder.Property(u => u.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.PartnerID)
                   .IsRequired();

        }
    }
}