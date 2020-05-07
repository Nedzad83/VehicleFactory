using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleFactory.Controllers;
using VehicleFactory.Controllers.Resources;
using VehicleFactory.Models;
using VehicleFactory.Persistence;

namespace VehicleFactory.Controllers
{
    public class FeaturesController : Controller
    {
        private readonly VehicleDbContext context;
        private readonly IMapper mapper;
        public FeaturesController(VehicleDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("/api/features")]
        public async Task<IEnumerable<KeyValuePairResource>> GetFeatures()
        {
            var features = await context.Features.ToListAsync();

            return mapper.Map<List<Feature>, List<KeyValuePairResource>>(features);
        }

    }
    
}