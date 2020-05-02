using System.Collections.Generic;
using System.Collections.ObjectModel;
using VehicleFactory.Models;

namespace VehicleFactory.Controllers.Resources
{
    public class MakeResource
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ModelResource> Models { get; set; }

        public MakeResource()
        {
            Models = new Collection<ModelResource>();
        }
    }
}