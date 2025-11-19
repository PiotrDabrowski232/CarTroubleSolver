using AutoMapper;
using CarTroubleSolver.Logic.Dto.Car;
using CarTroubleSolver.Shared.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Functions.Car.Command
{
    public class AddCarCommand : IRequest<CarDto>
    {
        public CarDto Car { get; set; }
        public AddCarCommand(CarDto car) { Car = car; }
    }

    public class AddCarCommandHandler : IRequestHandler<AddCarCommand, CarDto>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AddCarCommandHandler(ICarRepository carRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CarDto> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            var car = _mapper.Map<Shared.Models.UserPanel.Car>(request.Car);

            car.OwnerId = Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            car.Id = Guid.NewGuid();

            var result = await _carRepository.Add(car);

            return result != null ? request.Car : new CarDto();
        }
    }
}
