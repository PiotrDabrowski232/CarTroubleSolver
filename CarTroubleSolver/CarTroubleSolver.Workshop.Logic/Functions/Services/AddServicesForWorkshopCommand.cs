using AutoMapper;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using CarTroubleSolver.Workshop.Logic.Dto.Service;
using MediatR;

namespace CarTroubleSolver.Workshop.Logic.Functions.Services
{
    public class AddServicesForWorkshopCommand(Guid id, IEnumerable<ServiceDto> services) : IRequest<IEnumerable<ServiceDto>>
    {
        public Guid Id { get; set; } = id;
        public IEnumerable<ServiceDto> Services { get; set; } = services;
    }

    public class AddServicesForWorkshopCommandHandler : IRequestHandler<AddServicesForWorkshopCommand, IEnumerable<ServiceDto>>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddServicesForWorkshopCommandHandler(CarTroubleSolverDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceDto>> Handle(AddServicesForWorkshopCommand request, CancellationToken cancellationToken)
        {
            var existServices = _dbContext.WorkshopServices
                .Where(x => x.WorkshopId == request.Id);

            if (existServices.Any())
                _dbContext.WorkshopServices.RemoveRange(existServices);


            var services = request.Services.Select(service =>
            {
                var workshopService = _mapper.Map<WorkshopServices>(service);
                workshopService.WorkshopId = request.Id;
                return workshopService;
            }).ToList();

            await _dbContext.WorkshopServices.AddRangeAsync(services);

            await _dbContext.SaveChangesAsync();

            return request.Services;
        }
    }

}
