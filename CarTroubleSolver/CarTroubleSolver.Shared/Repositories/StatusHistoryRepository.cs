using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.ExtraModels;
using CarTroubleSolver.Shared.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Shared.Repositories
{
    public class StatusHistoryRepository(CarTroubleSolverDbContext dbContext) : GenericRepository<StatusHistory>(dbContext), IGenericRepository<StatusHistory>, IStatusHistoryRepository
    {
        public async Task<(string, DateTime, int)> GetNewestAccidentStatus(Guid accidentId, CancellationToken cancellationToken)
        {
            var status = await _context.StatusHistory
                .Where(x => x.AccidentId == accidentId)
                .OrderByDescending(x => x.ControlQueue)
                .FirstOrDefaultAsync(cancellationToken);

            return (Enum.GetName(status.Status), status.Date, status.ControlQueue);
        }
    }
}
