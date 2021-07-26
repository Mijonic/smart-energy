using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergy.MicroserviceAPI.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.Infrastructure
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            //Set table name mapping
            // builder.ToTable("ListItems");

            //Set FK
            builder.HasKey(i => i.ID);

            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.NotificationType)
                .HasConversion<String>()
                .IsRequired();

            builder.Property(i => i.Timestamp)
                  .ValueGeneratedOnAdd()
                  .IsRequired();

            builder.Property(i => i.IsRead)
                  .IsRequired();

            builder.Property(i => i.Description)
                  .HasMaxLength(100)
                  .IsRequired();

            builder.HasOne(i => i.User)
                .WithMany(p => p.Notifications)
                .HasForeignKey(i => i.UserID)
                .IsRequired();

            builder.HasOne(i => i.NotificationAnchor)
               .WithMany(p => p.Notifications)
               .HasForeignKey(i => i.NotificationAnchorID)
               .IsRequired();

        }
    }
}
