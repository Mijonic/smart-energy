using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergyDomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Infrastructure.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            //Set table name mapping
            // builder.ToTable("ListItems");

            //Set FK
            builder.HasKey(i => i.ID);

            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.Street)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.City)
              .IsRequired()
              .HasMaxLength(50);

            builder.Property(i => i.Zip)
              .IsRequired()
              .HasMaxLength(50);

            builder.Property(i => i.MorningPriority)
              .IsRequired()
              .HasMaxLength(5);

            builder.Property(i => i.NoonPriority)
              .IsRequired()
              .HasMaxLength(5);

            builder.Property(i => i.NightPriority)
              .IsRequired()
              .HasMaxLength(5);

            builder.Property(i => i.Longitude)
              .IsRequired(false);

            builder.Property(i => i.Latitude)
             .IsRequired(false);

        }
    }
}
