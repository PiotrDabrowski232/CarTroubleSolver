using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Workshop.Logic.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Workshop.Logic.Functions.Repairs
{
    public class GetRepairHistoryQuery(string accidentId) : IRequest<RepairHistoryDto>
    {
        public string AccdentId { get; set; } = accidentId;
    }

    public class GetRepairHistoryQueryHandler : IRequestHandler<GetRepairHistoryQuery, RepairHistoryDto>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        public GetRepairHistoryQueryHandler(CarTroubleSolverDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<RepairHistoryDto> Handle(GetRepairHistoryQuery request, CancellationToken cancellationToken)
        {
            var history = await _dbContext.RepairHistory
                .Where(x => x.AccidentId == Guid.Parse(request.AccdentId))
                .Include(x => x.HistoryItems)
                .FirstOrDefaultAsync(cancellationToken);

            RepairHistoryDto repairHistory = new RepairHistoryDto
            {
                Service = Enum.GetName(history.Service),
                Price = history.Price,
                SpentHours = history.SpentHours,
            };

            if (history.HistoryItems.Any())
                repairHistory.HistoryItems = history.HistoryItems
                    .Select(x => new HistoryItemDto
                    {
                        Name = x.Name,
                        Price = x.Price,
                        Quantity = x.Amount
                    }).ToList();

            return repairHistory;
        }
    }
}
