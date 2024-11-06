using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Repositories.Interfaces;
using CarTroubleSolver.Workshop.Logic.Dto.Accident;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Workshop.Logic.Functions.Accident
{
    public class GetAccidentFullInfoQuery(string accidentId) : IRequest<AccidentFullInfoDto>
    {
        public string AccidentId { get; set; } = accidentId;
    }

    public class GetAccidentFullInfoQueryHandler : IRequestHandler<GetAccidentFullInfoQuery, AccidentFullInfoDto>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly IStatusHistoryRepository _statusHistory;
        public GetAccidentFullInfoQueryHandler(CarTroubleSolverDbContext dbContext, IStatusHistoryRepository statusHistory)
        {
            _dbContext = dbContext;
            _statusHistory = statusHistory;
        }
        public async Task<AccidentFullInfoDto> Handle(GetAccidentFullInfoQuery request, CancellationToken cancellationToken)
        {
            var status = await _statusHistory.GetNewestAccidentStatus(Guid.Parse(request.AccidentId), cancellationToken);

            return await _dbContext.Accidents.Where(x => x.Id == Guid.Parse(request.AccidentId))
                .Select(x => new AccidentFullInfoDto
                {
                    Id = x.Id,
                    Service = Enum.GetName(x.Service),
                    Status = status.Item1,
                    Date = status.Item2,
                    UserMessage = x.ProblemDescription,
                    Car = new Dto.CarDto
                    {
                        Id = x.CarId,
                        Brand = Enum.GetName(x.Car.Brand),
                        Model = x.Car.Model,
                        Mileage = x.Car.Mileage,
                        Engine = x.Car.Engine
                    },
                    UserContact = x.Car.Owner.PhoneNumber.ToString(),
                    UserName = $"{x.Car.Owner.Name} {x.Car.Owner.Surname}"
                })
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
