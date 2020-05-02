using AutoMapper;
using VehicleFactory.Controllers.Resources;
using VehicleFactory.Models;

namespace VehicleFactory.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
        }
    }
}