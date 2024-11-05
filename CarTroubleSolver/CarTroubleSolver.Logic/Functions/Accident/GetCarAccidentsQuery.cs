using CarTroubleSolver.Logic.Dto.Accident;
using CarTroubleSolver.Shared.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Logic.Functions.Accident
{
    public class GetCarAccidentsQuery(long vin) : IRequest<List<AccidentBasicInfo>>
    {
        public long Vin { get; set; } = vin;
    }

    public class GetCarAccidentsQueryHandler : IRequestHandler<GetCarAccidentsQuery, List<AccidentBasicInfo>>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        public GetCarAccidentsQueryHandler(CarTroubleSolverDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AccidentBasicInfo>> Handle(GetCarAccidentsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Accidents.Where(x => x.Car.VIN == request.Vin)
                .Select(x => new AccidentBasicInfo
                {
                    Id = x.Id,
                    WorkshopName = x.Workshop.Name,
                    Service = Enum.GetName(x.Service),
                    Status = x.StatusHistory.OrderByDescending(x => x.Date).Select(x => Enum.GetName(x.Status)).FirstOrDefault(),
                    Date = x.StatusHistory.OrderByDescending(x => x.Date).Select(x => x.Date).FirstOrDefault()
                }).ToListAsync(cancellationToken);
        }
    }
}
