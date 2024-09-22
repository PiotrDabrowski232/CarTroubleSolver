using AutoMapper;
using CarTroubleSolver.Shared.Repositories.Interfaces;
using CarTroubleSolver.Shared.Services.Interface;
using CarTroubleSolver.Workshop.Logic.Dto.Workshop;
using MediatR;

namespace CarTroubleSolver.Workshop.Logic.Functions.Workshop.Command
{
    public class RegiserWorkshopCommand : IRequest<Guid>
    {
        public RegisterWorkshopDto Workshop { get; set; }

        public RegiserWorkshopCommand(RegisterWorkshopDto workshop)
        {
            Workshop = workshop;
        }
    }
    public class AddCarCommandHandler(IWorkshopRepository workshopRepository,
        IMapper mapper, IGeoLocalizationService geoLocalizationService,
        IHashingService hashingService) : IRequestHandler<RegiserWorkshopCommand, Guid>
    {
        private readonly IWorkshopRepository _workshopRepository = workshopRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IGeoLocalizationService _geoLocalizationService = geoLocalizationService;
        private readonly IHashingService _hashingService = hashingService;

        public Task<Guid> Handle(RegiserWorkshopCommand request, CancellationToken cancellationToken)
        {
            var workshop = _mapper.Map<Shared.Models.WorkshopPanel.Workshop>(request.Workshop);

            var geoLocalization = _geoLocalizationService.GetCurrentGeoLocalization(request.Workshop.Street, cancellationToken).Result;

            workshop.Longitude = geoLocalization.Longitude;
            workshop.Latitude = geoLocalization.Latitude;

            workshop.Password = _hashingService.HashPassword(null, workshop.Password);

            var result = _workshopRepository.Add(workshop);

            return Task.FromResult(result);
        }
    }
}
