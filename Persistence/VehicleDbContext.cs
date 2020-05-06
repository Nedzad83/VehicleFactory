using Microsoft.EntityFrameworkCore;
using VehicleFactory.Models;

namespace VehicleFactory.Persistence
{
    public class VehicleDbContext : DbContext
    {
        public DbSet<Make>  Makes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public VehicleDbContext(DbContextOptions options)
        : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFeature>()
                .HasKey(vf => new { vf.VehicleId, vf.FeatureId });
        }

    }
}