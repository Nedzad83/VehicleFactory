using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleFactory.Controllers.Resources;
using VehicleFactory.Models;
using VehicleFactory.Persistence;

namespace VehicleFactory.Controllers
{
    //[Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private VehicleDbContext context { get; }
        private IMapper mapper { get; }
        public VehiclesController(IMapper mapper, VehicleDbContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpPost("/api/vehicles")]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleResource vehicleResource)
        {
            var vehicle = mapper.Map<VehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;
            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();

            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }
    }
}