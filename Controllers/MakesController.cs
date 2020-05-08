using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleFactory.Controllers.Resources;
using VehicleFactory.Core.Models;
using VehicleFactory.Persistence;

namespace VehicleFactory.Controllers
{
    public class MakesController : Controller
    {
        private IMapper mapper { get; }
        private VehicleDbContext context { get; }
        public MakesController(VehicleDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }


        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeResource>> GetMakes()
        {
            var makes = await context.Makes.Include(x => x.Models).ToListAsync();

            return mapper.Map<List<Make>, List<MakeResource>>(makes);
        }
    }
}