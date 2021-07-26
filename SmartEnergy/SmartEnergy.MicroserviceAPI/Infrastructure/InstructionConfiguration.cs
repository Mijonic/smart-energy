using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartEnergy.MicroserviceAPI.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.MicroserviceAPI.Infrastructure
{
    public class InstructionConfiguration : IEntityTypeConfiguration<Instruction>
    {
        public void Configure(EntityTypeBuilder<Instruction> builder)
        {
            //Set table name mapping
            // builder.ToTable("ListItems");

            //Set FK
            builder.HasKey(i => i.ID);

            builder.Property(i => i.ID)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.IsExecuted)
                  .IsRequired();

            builder.Property(i => i.Description)
                  .HasMaxLength(100)
                  .IsRequired();

            builder.HasOne(i => i.WorkPlan)
                .WithMany(p => p.Instructions)
                .HasForeignKey(i => i.WorkPlanID)
                .IsRequired();

            //builder.HasOne(i => i.Device)
            //   .WithMany(p => p.Instructions)
            //   .HasForeignKey(i => i.DeviceID)
            //   .IsRequired();

        }
    }
}
