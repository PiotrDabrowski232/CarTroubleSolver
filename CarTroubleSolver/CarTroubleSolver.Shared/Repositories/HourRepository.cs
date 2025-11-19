using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using CarTroubleSolver.Shared.Repositories.Interfaces;

namespace CarTroubleSolver.Shared.Repositories
{
    public class HourRepository(CarTroubleSolverDbContext dbContext) : GenericRepository<HourConfiguration>(dbContext), IGenericRepository<HourConfiguration>, IHourRepository
    {
        public Task AddRange(IEnumerable<HourConfiguration> hours)
        {
            dbContext.AddRangeAsync(hours);
            return dbContext.SaveChangesAsync();
        }
    }
}
