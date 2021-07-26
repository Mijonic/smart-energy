using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergy.MicroserviceAPI.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.Infrastructure
{
    public class MultimediaAttachmentConfiguration : IEntityTypeConfiguration<MultimediaAttachment>
    {
        public void Configure(EntityTypeBuilder<MultimediaAttachment> builder)
        {
            //Set table name mapping
            // builder.ToTable("ListItems");

            //Set FK
            builder.HasKey(i => i.ID);

            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();

            // add unique
            builder.Property(i => i.Url)
                .IsRequired();

            builder.HasOne(i => i.MultimediaAnchor)
                 .WithMany(p => p.MultimediaAttachments)
                 .HasForeignKey(i => i.MultimediaAnchorID)
                 .IsRequired();
        }
    }
}
