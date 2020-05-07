using System.Threading.Tasks;
using VehicleFactory.Core;

namespace VehicleFactory.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VehicleDbContext context;

        public UnitOfWork(VehicleDbContext context)
        {
            this.context = context;
        }

        public async Task CompleteAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}