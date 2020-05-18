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

        public async Task<IEnumerable<Vehicle>> GetVehicles(VehicleQuery queryObj) 
        {
            var query = context.Vehicles
                    .Include(v => v.Model)
                        .ThenInclude(m => m.Make)
                    .Include(f => f.Features)
                        .ThenInclude(vf => vf.Feature)
                    .AsQueryable();
            if (queryObj.MakeId.HasValue)
                query = query.Where(x=>x.Model.MakeId == queryObj.MakeId.Value);
            
            if (queryObj.ModelId.HasValue)
                query = query.Where(x=>x.ModelId == queryObj.ModelId.Value);

            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            { 
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
                ["id"] = v => v.Id
            };

            if (queryObj.IsSortAscending)
                query = query.OrderBy(columnsMap[queryObj.SortBy]);
            else
                query = query.OrderByDescending(columnsMap[queryObj.SortBy]);

            // columnsMap.Add("make", v => v.Model.Make.Name);
            // columnsMap.Add("model", v => v.Model.Name);
            // columnsMap.Add("contactName", v => v.ContactName);
            // columnsMap.Add("id", v => v.Id);

            // if (queryObj.SortBy == "make")
            //     query = (queryObj.IsSortAscending) ? query.OrderBy(v => v.Model.Make.Name) : query.OrderByDescending(v=>v.Model.Make.Name);
            // if (queryObj.SortBy == "model")
            //     query = (queryObj.IsSortAscending) ? query.OrderBy(v => v.Model.Name) : query.OrderByDescending(v=>v.Model.Name);
            // if (queryObj.SortBy == "contactName")
            //     query = (queryObj.IsSortAscending) ? query.OrderBy(v => v.ContactName) : query.OrderByDescending(v=>v.ContactName);
            // if (queryObj.SortBy == "id")
            //     query = (queryObj.IsSortAscending) ? query.OrderBy(v => v.Id) : query.OrderByDescending(v=>v.Id);

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