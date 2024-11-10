using CarTroubleSolver.Logic.Dto;
using CarTroubleSolver.Logic.Dto.Workshop;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.Enum;
using CarTroubleSolver.Shared.Services.Interface;
using GeoCoordinatePortable;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Logic.Functions.Workshop
{
    public class WorkshopInfoQuery(GeoDto? geo) : IRequest<List<WorkshopInfoDto>>
    {
        public GeoDto? Geo { get; set; } = geo;
    }

    public class WorkshopInfoQueryHandler : IRequestHandler<WorkshopInfoQuery, List<WorkshopInfoDto>>
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly IGeoLocalizationService _geoLocalizationService;

        public WorkshopInfoQueryHandler(CarTroubleSolverDbContext dbContext, IGeoLocalizationService geoLocalizationService)
        {
            _dbContext = dbContext;
            _geoLocalizationService = geoLocalizationService;
        }

        public async Task<List<WorkshopInfoDto>> Handle(WorkshopInfoQuery request, CancellationToken cancellationToken)
        {
            var workshops = await _dbContext
                .Workshops
                .Where(x => x.Services.Any())
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    Services = x.Services.Select(service => service.Service).ToList(),
                    Rating = x.Ratings.Any() ? x.Ratings.Average(r => r.Rate) : 0,
                    X = x.Location.X,
                    Y = x.Location.Y,
                })
                .ToListAsync(cancellationToken);

            var workshopList = new List<WorkshopInfoDto>();

            foreach (var item in workshops)
            {
                var cityDetails = await _geoLocalizationService.GetLocalizationDetails(item.X, item.Y, cancellationToken);

                string cityName = cityDetails?.City ?? "Unknown";

                var workshopDto = new WorkshopInfoDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Services = item.Services.Select(service => Enum.GetName(typeof(ServiceType), service)).ToList(),
                    Rating = item.Rating,
                    City = cityName,
                };

                if (request.Geo != null && request.Geo.Latitude != 0 && request.Geo.Longitude != 0)
                {
                    var geoCoord1 = new GeoCoordinate(request.Geo.Longitude, request.Geo.Latitude);
                    var geoCoord2 = new GeoCoordinate(item.X, item.Y);

                    workshopDto.Distance = geoCoord1.GetDistanceTo(geoCoord2)/1000;
                }

                workshopList.Add(workshopDto);
            }

            return workshopList;
        }
    }
}
