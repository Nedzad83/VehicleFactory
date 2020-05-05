using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using VehicleFactory.Models;

namespace VehicleFactory.Controllers.Resources
{
    public partial class VehicleResource
    {

        public int Id { get; set; }
        public int ModelId { get; set; }
        public bool IsRegistered { get; set; }

        public ContactResource Contact { get; set; }
        public ICollection<int> Features { get; set; }

        public VehicleResource()
        {
            Features = new Collection<int>();
        }

    }
}