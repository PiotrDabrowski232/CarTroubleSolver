using CarTroubleSolver.Logic.Dto;
using CarTroubleSolver.Shared.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarTroubleSolver.Logic.Functions.StausHistory
{
    public class GetStatusHistoryQuery(string accidentId) : IRequest<List<StatusHistoryDto>>
    {
        public string AccidentId { get; set; } = accidentId;
    }

    public class GetStatusHistoryQueryHandler : IRequestHandler<GetStatusHistoryQuery, List<StatusHistoryDto>>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        public GetStatusHistoryQueryHandler(CarTroubleSolverDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<StatusHistoryDto>> Handle(GetStatusHistoryQuery request, CancellationToken cancellationToken)
        {
            var result =  await _dbContext.StatusHistory
                .Where(x => x.AccidentId == Guid.Parse(request.AccidentId))
                .OrderBy(x => x.ControlQueue)
                .Select(x => new StatusHistoryDto
                {
                    Id = x.Id,
                    Status = Enum.GetName(x.Status),
                    Date = x.Date
                })
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
