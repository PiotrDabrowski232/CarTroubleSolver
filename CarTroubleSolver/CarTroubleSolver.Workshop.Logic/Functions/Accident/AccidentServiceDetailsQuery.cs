using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Workshop.Logic.Dto.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Workshop.Logic.Functions.Accident
{
    public class AccidentServiceDetailsQuery(string accidentId) : IRequest<ServiceDto>
    {
        public string AccidentId { get; set; } = accidentId;
    }

    public class AccidentServiceDetailsQueryHandler : IRequestHandler<AccidentServiceDetailsQuery, ServiceDto>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        public AccidentServiceDetailsQueryHandler(CarTroubleSolverDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceDto> Handle(AccidentServiceDetailsQuery request, CancellationToken cancellationToken)
        {
            var accident = await _dbContext.Accidents.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.AccidentId));

            return await _dbContext.WorkshopServices
                .Where(x => x.WorkshopId == accident.WorkshopId && x.Service == accident.Service)
                .Select(x => new ServiceDto
                {
                    Price = x.Price,
                    ServiceType = Enum.GetName(x.Service)
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
