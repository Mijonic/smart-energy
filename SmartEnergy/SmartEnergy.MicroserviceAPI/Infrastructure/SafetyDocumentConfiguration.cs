using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergy.MicroserviceAPI.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.Infrastructure
{
    public class SafetyDocumentConfiguration : IEntityTypeConfiguration<SafetyDocument>
    {
        public void Configure(EntityTypeBuilder<SafetyDocument> builder)
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

            builder.Property(i => i.CreatedOn)
                  .ValueGeneratedOnAdd()
                  .IsRequired();

            builder.Property(i => i.OperationCompleted)
                  .IsRequired();

            builder.Property(i => i.GroundingRemoved)
                  .IsRequired();

            builder.Property(i => i.TagsRemoved)
                  .IsRequired();

            builder.Property(i => i.Ready)
                  .IsRequired();

            builder.Property(i => i.Details)
                  .HasMaxLength(100)
                  .IsRequired(false);

            builder.Property(i => i.Notes)
                  .IsRequired(false)
                  .HasMaxLength(100);

            builder.Property(i => i.Phone)
                  .HasMaxLength(30)
                  .IsRequired(false);


            builder.HasOne(i => i.User)
                .WithMany(p => p.SafetyDocuments)
                .HasForeignKey(i => i.UserID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(i => i.WorkPlan)
                .WithMany(p => p.SafetyDocuments)
                .HasForeignKey(i => i.WorkPlanID)
                .IsRequired(true);

            builder.HasOne(i => i.MultimediaAnchor)
                .WithOne(p => p.SafetyDocument)
                .HasForeignKey<SafetyDocument>(i => i.MultimediaAnchorID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(i => i.NotificationAnchor)
               .WithOne(p => p.SafetyDocument)
               .HasForeignKey<SafetyDocument>(i => i.NotificationAnchorID)
               .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(i => i.StateChangeAnchor)
               .WithOne(p => p.SafetyDocument)
               .HasForeignKey<SafetyDocument>(i => i.StateChangeAnchorID)
               .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
