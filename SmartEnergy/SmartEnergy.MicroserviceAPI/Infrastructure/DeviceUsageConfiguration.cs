using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergy.MicroserviceAPI.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.Infrastructure
{
    public class DeviceUsageConfiguration : IEntityTypeConfiguration<DeviceUsage>
    {
        public void Configure(EntityTypeBuilder<DeviceUsage> builder)
        {
            //Set table name mapping
            // builder.ToTable("ListItems");

            //Set FK
            builder.HasKey(i => i.ID);

            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();

            //builder.HasOne(i => i.Device)
            //    .WithMany(p => p.DeviceUsage)
            //    .HasForeignKey(i => i.DeviceID)
            //    .IsRequired();

            builder.HasOne(i => i.Incident)
                .WithMany(p => p.IncidentDevices)
                .HasForeignKey(i => i.IncidentID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);

            builder.HasOne(i => i.WorkRequest)
                .WithMany(p => p.DeviceUsage)
                .HasForeignKey(i => i.WorkRequestID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);

            builder.HasOne(i => i.WorkPlan)
                .WithMany(p => p.WorkPlanDevices)
                .HasForeignKey(i => i.WorkPlanID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);

            builder.HasOne(i => i.SafetyDocument)
                .WithMany(p => p.DeviceUsages)
                .HasForeignKey(i => i.SafetyDocumentID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .IsRequired(false);

        }
    }
}
