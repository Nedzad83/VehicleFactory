using System.Xml;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleFactory.Controllers.Resources;
using VehicleFactory.Core;
using VehicleFactory.Core.Models;
using System.Linq.Expressions;
using System;
using VehicleFactory.Extensions;

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

        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObj)
        {
            var result = new QueryResult<Vehicle>();
            var query = context.Vehicles
                    .Include(v => v.Model)
                        .ThenInclude(m => m.Make)
                    .Include(f => f.Features)
                        .ThenInclude(vf => vf.Feature)
                    .AsQueryable();
            if (queryObj.MakeId.HasValue)
                query = query.Where(x => x.Model.MakeId == queryObj.MakeId.Value);

            if (queryObj.ModelId.HasValue)
                query = query.Where(x => x.ModelId == queryObj.ModelId.Value);

            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName
            };

            query = query.ApplyOrdering( queryObj, columnsMap );
            result.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            
            result.Items = await query.ToListAsync();
            return result;
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