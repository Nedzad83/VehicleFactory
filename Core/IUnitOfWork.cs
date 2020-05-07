using System.Threading.Tasks;

namespace VehicleFactory.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}