using System.ComponentModel.DataAnnotations;

namespace VehicleFactory.Controllers.Resources
{
    public class PhotoResource
    {
        public int Id { get; set; }

        public string FileName { get; set; }
    }
}