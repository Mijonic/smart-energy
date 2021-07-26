using Microsoft.EntityFrameworkCore;
using SmartEnergy.LocationAPI.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEnergy.LocationAPI.Infrastructure
{
    public class LocationDbContext : DbContext
    {
        public LocationDbContext(DbContextOptions options) : base(options)
        {
        }

       
        public DbSet<Location> Location { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LocationDbContext).Assembly);

        }



    }
}
