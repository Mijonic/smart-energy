using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergy.MicroserviceAPI.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.Infrastructure
{
    public class WorkRequestConfiguration : IEntityTypeConfiguration<WorkRequest>
    {
        public void Configure(EntityTypeBuilder<WorkRequest> builder)
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
                  .IsRequired();

            builder.Property(i => i.EndDate)
                  .IsRequired();

            builder.Property(i => i.CreatedOn)
                  .IsRequired(false)
                  .HasDefaultValueSql("getdate()");

            builder.Property(i => i.Purpose)
                  .HasMaxLength(100)
                  .IsRequired();

            builder.Property(i => i.Note)
                  .IsRequired(false)
                  .HasMaxLength(100);

            builder.Property(i => i.Details)
                 .IsRequired(false)
                 .HasMaxLength(100);

            builder.Property(i => i.IsEmergency)
                  .IsRequired();

            builder.Property(i => i.CompanyName)
                  .HasMaxLength(50)
                  .IsRequired(false);

            builder.Property(i => i.Phone)
                  .HasMaxLength(30)
                  .IsRequired(false);

            builder.Property(i => i.Street)
                  .HasMaxLength(50)
                  .IsRequired(false);

            builder.HasOne(i => i.User)
                .WithMany(p => p.WorkRequests)
                .HasForeignKey(i => i.UserID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);//Restrict user delete chain

            builder.HasOne(i => i.Incident)
                .WithOne(p => p.WorkRequest)
                .HasForeignKey<WorkRequest>(i => i.IncidentID)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(i => i.MultimediaAnchor)
                .WithOne(p => p.WorkRequest)
                .HasForeignKey<WorkRequest>(i => i.MultimediaAnchorID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);


            builder.HasOne(i => i.NotificationsAnchor)
               .WithOne(p => p.WorkRequest)
               .HasForeignKey<WorkRequest>(i => i.NotificationAnchorID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(i => i.StateChangeAnchor)
              .WithOne(p => p.WorkRequest)
              .HasForeignKey<WorkRequest>(i => i.StateChangeAnchorID)
              .IsRequired(false)
              .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
