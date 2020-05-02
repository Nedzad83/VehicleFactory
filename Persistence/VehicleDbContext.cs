using Microsoft.EntityFrameworkCore;
using VehicleFactory.Models;

namespace VehicleFactory.Persistence
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext(DbContextOptions options)
        : base(options)
        {
            
        }
        public DbSet<Make>  Makes { get; set; }
    }
}