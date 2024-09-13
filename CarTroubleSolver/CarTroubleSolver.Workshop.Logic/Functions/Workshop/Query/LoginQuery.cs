using CarTroubleSolver.Shared.Exceptions;
using CarTroubleSolver.Shared.Services.Interface;
using CarTroubleSolver.Workshop.Data.Repositories.Interfaces;
using CarTroubleSolver.Workshop.Logic.Dto.Workshop;
using MediatR;

namespace CarTroubleSolver.Workshop.Logic.Functions.Workshop.Query
{
    public class LoginQuery : IRequest<string>
    {
        public LoginWorkshopDto Workshop { get; set; }

        public LoginQuery(LoginWorkshopDto workshop)
        {
            Workshop = workshop;
        }
    }
    public class LoginQueryHandler(IWorkshopRepository workshopRepository,
        IHashingService hashingService, ITokenService tokenService) : IRequestHandler<LoginQuery, string>
    {
        private readonly IWorkshopRepository _workshopRepository = workshopRepository;
        private readonly IHashingService _hashingService = hashingService;
        private readonly ITokenService _tokenService = tokenService;
        public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var data = _workshopRepository.GetAll()
                .Where(x => x.Email == request.Workshop.Email)
                .Select(x => new
                {
                    Id = x.Id.ToString(),
                    Password = x.Password,
                    Name = x.Name
                }).FirstOrDefault();

            if (data == null)
                throw new InvalidProvidedDataException("There is not workshop with provided email");

            if(!_hashingService.VerifyHashedPassword(null, data.Password, request.Workshop.Password))
                throw new InvalidProvidedDataException("Incorrect Password");

            return _tokenService.GenerateJwt(data.Id, data.Name, request.Workshop.Email);

        }
    }
}
