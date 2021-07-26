using Microsoft.EntityFrameworkCore;
using SmartEnergy.Infrastructure.Configurations;
using SmartEnergyDomainModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartEnergy.Infrastructure
{
    public class SmartEnergyDbContext : DbContext
    {
        public SmartEnergyDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Settings> Settings { get; set; }
        public DbSet<Icon> Icons { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Resolution> Resolutions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DeviceUsage> DeviceUsages { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<MultimediaAnchor> MultimediaAnchors { get; set; }
        public DbSet<MultimediaAttachment> MultimediaAttachments { get; set; }
        public DbSet<NotificationAnchor> NotificationAnchors { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<SafetyDocument> SafetyDocuments { get; set; }
        public DbSet<StateChangeAnchor> StateChangeAnchors { get; set; }
        public DbSet<StateChangeHistory> StateChangeHistories { get; set; }
        public DbSet<WorkPlan> WorkPlans { get; set; }
        public DbSet<WorkRequest> WorkRequests { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*modelBuilder.ApplyConfiguration(new IconConfiguration());
            modelBuilder.ApplyConfiguration(new SettingsConfiguration());
            modelBuilder.ApplyConfiguration(new ConsumerConfiguration());
            modelBuilder.ApplyConfiguration(new CallConfiguration());
            modelBuilder.ApplyConfiguration(new CrewConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceConfiguration());
            modelBuilder.ApplyConfiguration(new IncidentConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration());
            modelBuilder.ApplyConfiguration(new ResolutionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceUsageConfiguration());
            modelBuilder.ApplyConfiguration(new InstructionConfiguration());
            modelBuilder.ApplyConfiguration(new MultimediaAnchorConfiguration());
            modelBuilder.ApplyConfiguration(new MultimediaAttachmentConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationAnchorConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new SafetyDocumentConfiguration());
            modelBuilder.ApplyConfiguration(new StateChangeAnchorConfiguration());
            modelBuilder.ApplyConfiguration(new StateChangeHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new WorkPlanConfiguration());
            modelBuilder.ApplyConfiguration(new WorkRequestConfiguration());*/

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SmartEnergyDbContext).Assembly);

        }
    }
}
