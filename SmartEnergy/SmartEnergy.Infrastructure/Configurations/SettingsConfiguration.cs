using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergyDomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Infrastructure.Configurations
{
    public class SettingsConfiguration : IEntityTypeConfiguration<Settings>
    {
        public void Configure(EntityTypeBuilder<Settings> builder)
        {
            //Set table name mapping
            // builder.ToTable("ListItems");

            //Set FK
            builder.HasKey(i => i.ID);

            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.IsDefault)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(i => i.ShowErrors)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(i => i.ShowInfo)
               .IsRequired()
               .HasDefaultValue(true);

            builder.Property(i => i.ShowNonRequiredFields)
               .IsRequired()
               .HasDefaultValue(true);

            builder.Property(i => i.ShowSuccess)
               .IsRequired()
               .HasDefaultValue(true);

            builder.Property(i => i.ShowWarnings)
               .IsRequired()
               .HasDefaultValue(true);

        }
    }
}
