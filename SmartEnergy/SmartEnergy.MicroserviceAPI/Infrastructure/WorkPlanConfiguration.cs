using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergy.MicroserviceAPI.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.Infrastructure
{
    public class WorkPlanConfiguration : IEntityTypeConfiguration<WorkPlan>
    {
        public void Configure(EntityTypeBuilder<WorkPlan> builder)
        {
            //Set table name mapping
            // builder.ToTable("ListItems");

            //Set FK
            builder.HasKey(i => i.ID);

            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.DocumentType)
                .HasConversion<String>()
                .IsRequired();

            builder.Property(i => i.DocumentStatus)
                 .HasConversion<String>()
                 .IsRequired();

            builder.Property(i => i.StartDate)
                  .IsRequired(false);

            builder.Property(i => i.EndDate)
                  .IsRequired(false);

            builder.Property(i => i.CreatedOn)
                  .IsRequired(false)
                  .HasDefaultValueSql("getdate()");

            builder.Property(i => i.Purpose)
                  .HasMaxLength(100)
                  .IsRequired();

            builder.Property(i => i.Notes)
                  .IsRequired(false)
                  .HasMaxLength(100);

            builder.Property(i => i.CompanyName)
                  .HasMaxLength(50)
                  .IsRequired(false);

            builder.Property(i => i.Phone)
                  .HasMaxLength(30)
                  .IsRequired(false);

            builder.Property(i => i.Street)
                  .HasMaxLength(50)
                  .IsRequired();

            builder.HasOne(i => i.User)
                .WithMany(p => p.WorkPlans)
                .HasForeignKey(i => i.UserID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);//Restrict user delete chain

            builder.HasOne(i => i.WorkRequest)
                .WithOne(p => p.WorkPlan)
                .HasForeignKey<WorkPlan>(i => i.WorkRequestID)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);//Restrict user delete chain;

            builder.HasOne(i => i.MultimediaAnchor)
                .WithOne(p => p.WorkPlan)
                .HasForeignKey<WorkPlan>(i => i.MultimediaAnchorID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(i => i.NotificationAnchor)
               .WithOne(p => p.WorkPlan)
               .HasForeignKey<WorkPlan>(i => i.NotificationAnchorID)
               .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(i => i.StateChangeAnchor)
              .WithOne(p => p.WorkPlan)
              .HasForeignKey<WorkPlan>(i => i.StateChangeAnchorID)
              .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
