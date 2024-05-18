using AutoMapper;
using CarTroubleSolver.Data.Repositories.Interfaces;
using CarTroubleSolver.Logic.Dto.Car;
using MediatR;
using CarTroubleSolver.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
namespace CarTroubleSolver.Logic.Functions.Car.Command
{
    public class AddCarCommand : IRequest<CarDto>
    {
        public CarDto CarDto { get; set; }
        public AddCarCommand() { }
        public AddCarCommand(CarDto car) {  CarDto = car; }
    }

    public class AddCarCommandHandler(ICarRepository carRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IRequestHandler<AddCarCommand, CarDto>
    {

        private readonly ICarRepository _carRepository = carRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public Task<CarDto> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            var car = _mapper.Map<Data.Models.Car>(request.CarDto);

            car.OwnerId = (Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));

            car.Id = Guid.NewGuid();

            return _carRepository.Add(car).IsCompletedSuccessfully ? Task.FromResult(request.CarDto) : throw new Exception();
          
        }
    }
}
