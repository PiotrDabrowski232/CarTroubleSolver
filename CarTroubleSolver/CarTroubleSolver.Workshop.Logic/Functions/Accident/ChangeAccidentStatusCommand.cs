using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.Enum;
using CarTroubleSolver.Shared.Models.ExtraModels;
using CarTroubleSolver.Shared.Repositories.Interfaces;
using CarTroubleSolver.Workshop.Logic.Dto;
using MediatR;

namespace CarTroubleSolver.Workshop.Logic.Functions.Accident
{
    public class ChangeAccidentStatusCommand(string accidentId, CreateRepairHistoryDto? repairHistory) : IRequest<bool>
    {
        public string AccidentId { get; set; } = accidentId;
        public CreateRepairHistoryDto? CreateRepairHistory { get; set; } = repairHistory;
    }

    public class ChangeAccidentStatusCommandHandler : IRequestHandler<ChangeAccidentStatusCommand, bool>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        public readonly IStatusHistoryRepository _statusHistoryRepository;
        public ChangeAccidentStatusCommandHandler(CarTroubleSolverDbContext dbContext, IStatusHistoryRepository statusHistoryRepository)
        {
            _dbContext = dbContext;
            _statusHistoryRepository = statusHistoryRepository;
        }

        public async Task<bool> Handle(ChangeAccidentStatusCommand request, CancellationToken cancellationToken)
        {
            var maxStatus = await _statusHistoryRepository.GetNewestAccidentStatus(Guid.Parse(request.AccidentId), cancellationToken);

            StatusHistory status = new StatusHistory
            {
                Id = Guid.NewGuid(),
                Date = DateTime.Now,
                AccidentId = Guid.Parse(request.AccidentId),
                ControlQueue = maxStatus.Item3 + 1
            };

            if (maxStatus.Item1 == Enum.GetName(AccidentStatus.WaitingForCar))
                status.Status = AccidentStatus.Progress;
            else if (maxStatus.Item1 == Enum.GetName(AccidentStatus.Progress))
                status.Status = AccidentStatus.ReadyToReceive;
            else if (maxStatus.Item1 == Enum.GetName(AccidentStatus.ReadyToReceive))
                status.Status = AccidentStatus.Retrieved;

            _dbContext.StatusHistory.Add(status);

            _dbContext.SaveChanges();

            return true;
        }
    }
}
