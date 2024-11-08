using CarTroubleSolver.Logic.Dto;
using CarTroubleSolver.Logic.Dto.Workshop;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Services.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Logic.Functions.Workshop
{
    public class WorkshopDetailsQuery(string workshopId) : IRequest<WorkshopDetailsDto>
    {
        public string WorkshopId { get; set; } = workshopId;
    }

    public class WorkshopDetailsQueryHandler : IRequestHandler<WorkshopDetailsQuery, WorkshopDetailsDto>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly IGeoLocalizationService _geoLocalizationService;
        public WorkshopDetailsQueryHandler(CarTroubleSolverDbContext dbContext, IGeoLocalizationService geoLocalizationService)
        {
            _dbContext = dbContext;
            _geoLocalizationService = geoLocalizationService;
        }
        public async Task<WorkshopDetailsDto> Handle(WorkshopDetailsQuery request, CancellationToken cancellationToken)
        {
            var result = await _dbContext.Workshops.Where(x => x.Id == Guid.Parse(request.WorkshopId))
                 .Select(x => new
                 {
                     Workshop = new WorkshopDetailsDto
                     {
                         Name = x.Name,
                         PhoneNumber = x.PhoneNumber,
                         NIP = x.NIP,
                         Services = x.Services
                         .Select(x => new WorkshopServicesDto
                         {
                             Service = Enum.GetName(x.Service),
                             Price = x.Price
                         })
                         .ToList(),
                         RateDetails = x.Ratings
                         .Select(x => new RateDetailsDto
                         {
                             Comment = x.Comment,
                             Rate = x.Rate,
                             Date = x.Date,
                             User = $"{x.User.Name} {x.User.Surname}"
                         })
                         .ToList(),
                     },
                     X = x.Location.X,
                     Y = x.Location.Y,
                 }).FirstOrDefaultAsync(cancellationToken);

            result.Workshop.Location = await _geoLocalizationService.GetLocalizationDetails(result.X, result.Y, cancellationToken);

            return result.Workshop;

        }
    }
}
