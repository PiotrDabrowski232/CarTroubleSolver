using CarTroubleSolver.Logic.Dto.Repairs;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Repositories.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Logic.Functions.RepairHistory
{
    public class GetCarRepairsHitoriesQuery(string vin) : IRequest<List<RepairHistoryDto>>
    {
        public string VIN { get; set; } = vin;
    }

    public class GetCarRepairsHitoriesQueryHandler : IRequestHandler<GetCarRepairsHitoriesQuery, List<RepairHistoryDto>>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly IStatusHistoryRepository _statusHistoryRepository;
        public GetCarRepairsHitoriesQueryHandler(CarTroubleSolverDbContext dbContext, IStatusHistoryRepository statusHistoryRepository)
        {
            _dbContext = dbContext;
            _statusHistoryRepository = statusHistoryRepository;
        }

        public async Task<List<RepairHistoryDto>> Handle(GetCarRepairsHitoriesQuery request, CancellationToken cancellationToken)
        {

            var carHistory = await _dbContext.Cars.Where(x => x.VIN == request.VIN)
                .Include(x => x.RepairHistory)
                    .ThenInclude(x => x.HistoryItems)
                .Include(x => x.RepairHistory)
                    .ThenInclude(x => x.Workshop)
                .FirstOrDefaultAsync(cancellationToken);

            List<RepairHistoryDto> repairs = new List<RepairHistoryDto>();

            foreach (var item in carHistory.RepairHistory) 
            {
                var date = await _statusHistoryRepository.GetNewestAccidentStatus(item.AccidentId, cancellationToken);

                repairs.Add(new RepairHistoryDto
                {
                    WorkshopId = item.WorkshopId,
                    WorkshopName = item.Workshop.Name,
                    WorkshopNumber = item.Workshop.PhoneNumber.ToString(),
                    Service = Enum.GetName(item.Service),
                    Price = item.Price,
                    SpentHours = item.SpentHours,
                    Date = date.Item2,
                    HistoryItems = item.HistoryItems.Select(x => new HistoryItemDto
                    {
                        Name = x.Name,
                        Price = x.Price,
                        Quantity = x.Amount
                    }).ToList(),
                });
            }

            return repairs;
        }
    }
}
