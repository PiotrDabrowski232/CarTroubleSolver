using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.Enum;
using CarTroubleSolver.Shared.Repositories.Interfaces;
using CarTroubleSolver.Workshop.Logic.Dto.Accident;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarTroubleSolver.Workshop.Logic.Functions.Accident
{
    public class GetAccidentsQuery : IRequest<List<AccidentBasicInfoDto>>;

    public class GetAccidentsQueryHandler : IRequestHandler<GetAccidentsQuery, List<AccidentBasicInfoDto>>
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IStatusHistoryRepository _statusHistoryRepository;
        private readonly CarTroubleSolverDbContext _DbContext;

        public GetAccidentsQueryHandler(IHttpContextAccessor httpContextAccessor, CarTroubleSolverDbContext dbContext, IStatusHistoryRepository statusHistoryRepository)
        {
            _contextAccessor = httpContextAccessor;
            _DbContext = dbContext;
            _statusHistoryRepository = statusHistoryRepository;
        }
        public async Task<List<AccidentBasicInfoDto>> Handle(GetAccidentsQuery request, CancellationToken cancellationToken)
        {
            var workshopId = (Guid.Parse(_contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

            var accidents = await _DbContext.Accidents
                .Where(x => x.WorkshopId == workshopId)
                .Select(x => new AccidentBasicInfoDto
                {
                    Id = x.Id,
                    Brand = Enum.GetName(x.Car.Brand),
                    Model = x.Car.Model,
                    Vin = x.Car.VIN,
                    Engine = x.Car.Engine,
                    Service = Enum.GetName(x.Service),
                    Status = x.StatusHistory.OrderByDescending(x => x.ControlQueue).Select(x => Enum.GetName(x.Status)).FirstOrDefault(),
                    Date = x.StatusHistory.OrderByDescending(x => x.ControlQueue).Select(x => x.Date).FirstOrDefault()
                }).ToListAsync(cancellationToken);

            if (accidents.Any())
                return accidents.OrderByDescending(x => x.Status).ThenByDescending(x => x.Date).ToList();
            else
                return new List<AccidentBasicInfoDto>();
        }
    }
}
