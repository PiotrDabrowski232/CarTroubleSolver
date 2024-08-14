using AutoMapper;
using CarTroubleSolver.Data.Repositories.Interfaces;
using CarTroubleSolver.Logic.Dto.Car;
using CarTroubleSolver.Logic.Services.Interfaces;
using MediatR;

namespace CarTroubleSolver.Logic.Functions.Car.Command
{
    public class UpdateCarDetailsCommand : IRequest
    {
        public UpdateCarDto Car { get; set; }

        public UpdateCarDetailsCommand(UpdateCarDto car)
        {
            Car = car;
        }
    }
    public class UpdateCarDetailsCommandHandler : IRequestHandler<UpdateCarDetailsCommand>
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        public UpdateCarDetailsCommandHandler(ICarRepository carRepository, IMapper mapper, IFileService fileService)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _fileService = fileService;
        }

        public async Task Handle(UpdateCarDetailsCommand request, CancellationToken cancellationToken)
        {
            var car = _mapper.Map<Data.Models.Car>(request.Car);

            var imagePath = _carRepository.UpdateCarByVinAsync(car, request.Car.OldVin);

            if(imagePath != null && request.Car.VIN != request.Car.OldVin)
               await _fileService.UpdateFilePath(request.Car.VIN, imagePath);
            
        }
    }
}
