// src/EngineService.Infrastructure/Data/EngineServiceDbContext.cs
using EngineService.Domain.Entities;
using EngineService.EngineService.Domain.Entitities;
using Microsoft.EntityFrameworkCore;

namespace EngineService.Infrastructure.Data
{
    /// <summary>
    /// EF Core DbContext; Domain katmanındaki entity’leri DbSet olarak tanımlar.
    /// </summary>
    public class EngineServiceDbContext : DbContext
    {
        public EngineServiceDbContext(DbContextOptions<EngineServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ServiceRecord> ServiceRecords { get; set; }
        public DbSet<ServiceFile> ServiceFiles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Vehicle ↔ ServiceRecord ilişkisi
            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.ServiceRecords)
                .WithOne(sr => sr.Vehicle)
                .HasForeignKey(sr => sr.VehicleId);

            // ServiceRecord ↔ ServiceFile ilişkisi
            modelBuilder.Entity<ServiceRecord>()
                .HasMany(sr => sr.Files)
                .WithOne(f => f.ServiceRecord)
                .HasForeignKey(f => f.ServiceRecordId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
