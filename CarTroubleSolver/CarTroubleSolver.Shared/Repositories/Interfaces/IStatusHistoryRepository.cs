using CarTroubleSolver.Shared.Models.ExtraModels;

namespace CarTroubleSolver.Shared.Repositories.Interfaces
{
    public interface IStatusHistoryRepository : IGenericRepository<StatusHistory>
    {
        public Task<(string, DateTime, int)> GetNewestAccidentStatus(Guid accidentId, CancellationToken cancellationToken);

    }
}
