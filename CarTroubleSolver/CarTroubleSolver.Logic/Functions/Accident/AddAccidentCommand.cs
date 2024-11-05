using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.ExtraModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Logic.Functions.Accident
{
    public class AddAccidentCommand(string messageId, bool isAccepted) : IRequest<bool>
    {
        public string MessageId { get; set; } = messageId;
        public bool IsAccepted { get; set; } = isAccepted;
    }
    public class AddAccidentCommandHandler : IRequestHandler<AddAccidentCommand, bool>
    {
        public CarTroubleSolverDbContext _dbContext;
        public AddAccidentCommandHandler(CarTroubleSolverDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Handle(AddAccidentCommand request, CancellationToken cancellationToken)
        {
            var message = await _dbContext.Messages.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.MessageId));

            if (request.IsAccepted)
            {
                var worksopHours = await _dbContext.Hours.Where(x => x.WorkshopId == message.SenderWorkshopId 
                && x.DayOfWeek == message.DateOfVisit.Value.DayOfWeek)
                    .FirstOrDefaultAsync(cancellationToken);
                
                Shared.Models.ExtraModels.Accident accident = new Shared.Models.ExtraModels.Accident
                {
                    Id = Guid.NewGuid(),
                    WorkshopId = (Guid)message.SenderWorkshopId,
                    CarId = (Guid)message.CarId,
                    Service = message.Service,
                    StartDate = (DateTime)message.DateOfVisit,
                };

                StatusHistory status = new StatusHistory
                {
                    Id = Guid.NewGuid(),
                    Accident = accident,
                    Date = worksopHours != null ? (DateTime)message.DateOfVisit.Value.AddHours(worksopHours.From.Hour).AddMinutes(worksopHours.From.Minute) : (DateTime)message.DateOfVisit,
                   Status = Shared.Models.Enum.AccidentStatus.WaitingForCar
                };

                _dbContext.Accidents.Add(accident);

                _dbContext.StatusHistory.Add(status);

                message.Responsed = true;

                _dbContext.SaveChanges();
            }
            else
            {
                message.Responsed = true;
            }

            return true;
        }
    }
}