using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergyDomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Infrastructure.Configurations
{
    public class IconConfiguration : IEntityTypeConfiguration<Icon>
    {
        public void Configure(EntityTypeBuilder<Icon> builder)
        {
            //Set table name mapping
           // builder.ToTable("ListItems");

            //Set FK
            builder.HasKey(i => i.ID);

            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.IconType)
                .HasConversion<String>()
                .IsRequired();

            builder.Property(i => i.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder.HasOne(i => i.Settings)
                .WithMany(p => p.Icons)
                .HasForeignKey(i => i.SettingsID)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull);

        }
    }
}
