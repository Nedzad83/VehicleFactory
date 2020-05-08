using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using VehicleFactory.Controllers.Resources;
using VehicleFactory.Core.Models;

namespace VehicleFactory.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain class to API resource..
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
                 .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                 .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));
            CreateMap<Vehicle, VehicleResource>()
                 .ForMember(vr=> vr.Make, opt => opt.MapFrom(v => v.Model.Make))
                 .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                 .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf=>new KeyValuePairResource{ Id = vf.Feature.Id, Name = vf.Feature.Name })));

            // API resource to Domain class..
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v=>v.Id, opt=>opt.Ignore())
                .ForMember(v=>v.ContactName, opt=>opt.MapFrom(vr => vr.Contact.Name))    
                .ForMember(v=>v.ContactPhone, opt=>opt.MapFrom(vr => vr.Contact.Phone))    
                .ForMember(v=>v.ContactEmail, opt=>opt.MapFrom(vr => vr.Contact.Email))    
                .ForMember(v=>v.Features, opt=>opt.Ignore())
                .AfterMap((vr,v)=>{
                    // remove unselected features
                    var removedFeatures = v.Features.Where(f=>!vr.Features.Contains(f.FeatureId));
                    foreach(var f in removedFeatures)
                        v.Features.Remove(f);

                    //Add new feature
                    var addedFeature = vr.Features.Where(id=>!v.Features.Any(f=>f.FeatureId == id)).Select(id=>new VehicleFeature{ FeatureId = id });
                    foreach(var f in addedFeature)
                        v.Features.Add(f);
                });
        }
    }
}