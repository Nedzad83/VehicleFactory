using VehicleFactory.Extensions;

namespace VehicleFactory.Core.Models
{
    public class VehicleQuery : IQueryObject
    {
        public int? MakeId { get; set; }

        public int? ModelId { get; set; }

        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}