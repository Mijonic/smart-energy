using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergyDomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Infrastructure.Configurations
{
    public class CallConfiguration : IEntityTypeConfiguration<Call>
    {
        public void Configure(EntityTypeBuilder<Call> builder)
        {
            //Set table name mapping
            // builder.ToTable("ListItems");

            //Set FK
            builder.HasKey(i => i.ID);

            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.CallReason)
                .HasConversion<String>()
                .IsRequired();

            builder.Property(i => i.Comment)
                .IsRequired(false);

            builder.Property(i => i.Hazard)
               .IsRequired();

            builder.HasOne(i => i.Location)
                .WithMany(p => p.Calls)
                .HasForeignKey(i => i.LocationID)
                .IsRequired(true);

            builder.HasOne(i => i.Consumer)
                .WithMany(p => p.Calls)
                .HasForeignKey(i => i.ConsumerID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(i => i.Incident)
              .WithMany(p => p.Calls)
              .HasForeignKey(i => i.IncidentID)
              .IsRequired(false)
              .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
