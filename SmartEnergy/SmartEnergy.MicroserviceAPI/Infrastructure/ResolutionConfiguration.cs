using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergy.MicroserviceAPI.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.Infrastructure
{
    public class ResolutionConfiguration : IEntityTypeConfiguration<Resolution>
    {
        public void Configure(EntityTypeBuilder<Resolution> builder)
        {
            //Set table name mapping
            // builder.ToTable("ListItems");

            //Set FK
            builder.HasKey(i => i.ID);

            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.Cause)
                .HasConversion<String>()
                .IsRequired();

            builder.Property(i => i.Subcause)
              .HasConversion<String>()
              .IsRequired();

            builder.Property(i => i.Construction)
              .HasConversion<String>()
              .IsRequired();

            builder.Property(i => i.Material)
              .HasConversion<String>()
              .IsRequired();

            builder.HasOne(i => i.Incident)
                .WithOne(p => p.Resolution)
                .HasForeignKey<Resolution>(i => i.IncidentID)
                .IsRequired();

        }
    }
}
