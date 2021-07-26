using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergyDomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Infrastructure.Configurations
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            //Set table name mapping
            // builder.ToTable("ListItems");

            //Set FK
            builder.HasKey(i => i.ID);

            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.DeviceType)
               .HasConversion<String>()
               .IsRequired();

            builder.Property(i => i.Name)
              .HasMaxLength(50)
              .IsRequired();

            builder.HasOne(i => i.Location)
              .WithMany(p => p.Devices)
              .HasForeignKey(i => i.LocationID)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);//To ensure accidental location delete doesn't propagate

        }
    }
}
