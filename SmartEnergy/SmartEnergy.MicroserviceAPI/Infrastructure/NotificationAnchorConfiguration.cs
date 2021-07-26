using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergy.MicroserviceAPI.DomainModels;

using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.Infrastructure
{
    public class NotificationAnchorConfiguration : IEntityTypeConfiguration<NotificationAnchor>
    {
        public void Configure(EntityTypeBuilder<NotificationAnchor> builder)
        {
            //Set table name mapping
            // builder.ToTable("ListItems");

            //Set FK
            builder.HasKey(i => i.ID);

            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();
        }
    }
}
