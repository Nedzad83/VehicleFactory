using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using VehicleFactory.Controllers.Resources;
using VehicleFactory.Models;

namespace VehicleFactory.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain class to API resource..
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();
            CreateMap<Vehicle, VehicleResource>()
                 .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                 .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

            // API resource to Domain class..
            CreateMap<VehicleResource, Vehicle>()
                .ForMember(v=>v.ContactName, opt=>opt.MapFrom(vr => vr.Contact.Name))    
                .ForMember(v=>v.ContactPhone, opt=>opt.MapFrom(vr => vr.Contact.Phone))    
                .ForMember(v=>v.ContactEmail, opt=>opt.MapFrom(vr => vr.Contact.Email))    
                .ForMember(v=>v.Features, opt=>opt.MapFrom(vr => vr.Features.Select(id => new VehicleFeature{ FeatureId = id })));
        }
    }
}