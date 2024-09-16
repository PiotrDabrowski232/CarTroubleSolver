using CarTroubleSolver.Shared.Exceptions;
using CarTroubleSolver.Shared.Services.Interface;
using CarTroubleSolver.Workshop.Data.Repositories.Interfaces;
using CarTroubleSolver.Workshop.Logic.Dto.Workshop;
using MediatR;

namespace CarTroubleSolver.Workshop.Logic.Functions.Workshop.Query
{
    public class LoginQuery : IRequest<LoginResponse>
    {
        public LoginWorkshopDto Workshop { get; set; }

        public LoginQuery(LoginWorkshopDto workshop)
        {
            Workshop = workshop;
        }
    }
    public class LoginQueryHandler(IWorkshopRepository workshopRepository,
        IHashingService hashingService, ITokenService tokenService,
        IGeoLocalizationService geoLocalizationService) : IRequestHandler<LoginQuery, LoginResponse>
    {
        private readonly IWorkshopRepository _workshopRepository = workshopRepository;
        private readonly IHashingService _hashingService = hashingService;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IGeoLocalizationService _geoLocalizationService = geoLocalizationService;
        public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var data = _workshopRepository.GetAll()
                .Where(x => x.Email == request.Workshop.Email)
                .Select(x => new
                {
                    Id = x.Id.ToString(),
                    Password = x.Password,
                    Longitude = x.Longitude,
                    Latitude = x.Latitude,
                    WorkshopDetails = new WorkshopDetailsDto
                    {
                        Name = x.Name,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                        NIP = x.NIP,
                    }
                }).FirstOrDefault();

            if (data == null)
                throw new InvalidProvidedDataException("There is not workshop with provided email");

            if(!_hashingService.VerifyHashedPassword(null, data.Password, request.Workshop.Password))
                throw new InvalidProvidedDataException("Incorrect Password");

            var token = _tokenService.GenerateJwt(data.Id, data.WorkshopDetails.Name, request.Workshop.Email);

            data.WorkshopDetails.Adress = await _geoLocalizationService.GetLocalizationDetails(data.Latitude, data.Longitude, cancellationToken);

            return new LoginResponse
            {
                JWT = token,
                WorkshopDetails = data.WorkshopDetails
            };
        }
    }
}
