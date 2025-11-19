using AutoMapper;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Workshop.Logic.Dto.Service;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Workshop.Logic.Functions.Services
{
    public class GetWorkshopServicesQuery(Guid id) : IRequest<IEnumerable<ServiceDto>>
    {
        public Guid Id { get; set; } = id;
    }

    public class GetWorkshopServicesQueryHandler : IRequestHandler<GetWorkshopServicesQuery, IEnumerable<ServiceDto>>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetWorkshopServicesQueryHandler(CarTroubleSolverDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceDto>> Handle(GetWorkshopServicesQuery request, CancellationToken cancellationToken)
        {
           var services = await _dbContext.WorkshopServices
                .Where(x => x.WorkshopId == request.Id)
                .Select(x => new ServiceDto
                {
                    Price = x.Price,
                    ServiceType = Enum.GetName(x.Service),
                }).ToListAsync(cancellationToken);

            return services.AsEnumerable();

        }
    }
}
