using CarTroubleSolver.Shared.Models.WorkshopPanel;

namespace CarTroubleSolver.Shared.Repositories.Interfaces
{
    public interface IHourRepository : IGenericRepository<HourConfiguration>
    {
        public Task AddRange(IEnumerable<HourConfiguration> hours);
    }
}
