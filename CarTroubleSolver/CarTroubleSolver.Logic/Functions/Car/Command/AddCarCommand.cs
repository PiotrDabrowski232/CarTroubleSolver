using AutoMapper;
using CarTroubleSolver.Logic.Dto.Car;
using CarTroubleSolver.Shared.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CarTroubleSolver.Logic.Functions.Car.Command
{
    public class AddCarCommand : IRequest<CarDto>
    {
        public CarDto Car { get; set; }
        public AddCarCommand(CarDto car) { Car = car; }
    }

    public class AddCarCommandHandler(ICarRepository carRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IRequestHandler<AddCarCommand, CarDto>
    {

        private readonly ICarRepository _carRepository = carRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public Task<CarDto> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            var car = _mapper.Map<Shared.Models.UserPanel.Car>(request.Car);

            car.OwnerId = (Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

            car.Id = Guid.NewGuid();

            return _carRepository.Add(car).IsCompletedSuccessfully ? Task.FromResult(request.Car) : throw new Exception();

        }
    }
}
