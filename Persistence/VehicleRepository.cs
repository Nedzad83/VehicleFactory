using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleFactory.Core;
using VehicleFactory.Models;

namespace VehicleFactory.Persistence
{

    public class VehicleRepository : IVehicleRepository
    {
        private readonly VehicleDbContext context;
        public VehicleRepository(VehicleDbContext context)
        {
            this.context = context;
        }
        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Vehicles.FindAsync(id);
                
            return await context.Vehicles
                     .Include(v => v.Features)
                         .ThenInclude(vf => vf.Feature)
                     .Include(m => m.Model)
                         .ThenInclude(x => x.Make)
                     .SingleOrDefaultAsync(v => v.Id == id);
        }

        public void Add(Vehicle vehicle) 
        {
            context.Add(vehicle);
        }

        public void Remove(Vehicle vehicle)
        {
            context.Remove(vehicle);
        }
    }
}