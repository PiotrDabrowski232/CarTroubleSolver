using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.Enum;
using CarTroubleSolver.Shared.Models.ExtraModels;
using CarTroubleSolver.Workshop.Logic.Dto;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Workshop.Logic.Functions.Repairs
{
    public class UpdateRepairHistoryCommand(string accidentId, RepairHistoryDto history) : IRequest<bool>
    {
        public string AccidentId { get; set; } = accidentId;
        public RepairHistoryDto History { get; set; } = history;
    }

    public class UpdateRepairHistoryCommandHandler : IRequestHandler<UpdateRepairHistoryCommand, bool>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        public UpdateRepairHistoryCommandHandler(CarTroubleSolverDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(UpdateRepairHistoryCommand request, CancellationToken cancellationToken)
        {
            var repair = await _dbContext.RepairHistory
                .Where(x => x.AccidentId == Guid.Parse(request.AccidentId))
                .Include(x => x.HistoryItems)
                .FirstOrDefaultAsync(cancellationToken);

            RepairHistory history = new RepairHistory
            {
                Id = Guid.NewGuid(),
                Service = (ServiceType)Enum.Parse(typeof(ServiceType), request.History.Service),
                Price = request.History.Price,
                SpentHours = request.History.SpentHours,
                CarId = repair.CarId,
                WorkshopId = repair.WorkshopId,
                AccidentId = repair.AccidentId,
            };

            var items = request.History.HistoryItems
                .Select(item => new HistoryItems
                {
                    Id = Guid.NewGuid(),
                    Name = item.Name,
                    Price = item.Price,
                    Amount = item.Quantity,
                    RepairHistoryId = history.Id,
                }).ToList();


            _dbContext.HistoryItems.RemoveRange(repair.HistoryItems);
            _dbContext.RepairHistory.Remove(repair);

            _dbContext.RepairHistory.Add(history);
            _dbContext.HistoryItems.AddRange(items);


            _dbContext.SaveChanges();

            return true;
        }
    }
}
