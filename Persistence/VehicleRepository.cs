using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleFactory.Controllers.Resources;
using VehicleFactory.Core;
using VehicleFactory.Core.Models;

namespace VehicleFactory.Persistence
{

    public class VehicleRepository : IVehicleRepository
    {
        private readonly VehicleDbContext context;
        public VehicleRepository(VehicleDbContext context)
        {
            this.context = context;
        }
        public async Task<Core.Models.Vehicle> GetVehicle(int id, bool includeRelated = true)
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

        public async Task<IEnumerable<Vehicle>> GetVehicles(Filter filter) 
        {
            var query = context.Vehicles
                    .Include(v => v.Model)
                        .ThenInclude(m => m.Make)
                    .Include(f => f.Features)
                        .ThenInclude(vf => vf.Feature)
                    .AsQueryable();
            if (filter.MakeId.HasValue)
                query = query.Where(x=>x.Model.MakeId == filter.MakeId.Value);
            
            if (filter.ModelId.HasValue)
                query = query.Where(x=>x.ModelId == filter.ModelId.Value);


            return await query.ToListAsync();
        }

        public void Add(Core.Models.Vehicle vehicle) 
        {
            context.Add(vehicle);
        }

        public void Remove(Core.Models.Vehicle vehicle)
        {
            context.Remove(vehicle);
        }
    }
}