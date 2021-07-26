using Microsoft.EntityFrameworkCore;
using SmartEnergy.DeviceUsageAPI.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergy.DeviceUsageAPI.Infrastructure
{
    public class DeviceUsageDbContext : DbContext
    {
        public DeviceUsageDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<DeviceUsage> DeviceUsage { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeviceUsageDbContext).Assembly);

        }



    }
}
