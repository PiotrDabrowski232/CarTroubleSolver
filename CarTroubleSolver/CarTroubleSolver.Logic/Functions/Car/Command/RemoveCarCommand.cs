using CarTroubleSolver.Shared.Repositories.Interfaces;
using MediatR;

namespace CarTroubleSolver.Logic.Functions.Car.Command
{
    public class RemoveCarCommand : IRequest<bool>
    {
        public string VIN { get; set; }

        public RemoveCarCommand(string vin)
        {
            VIN = vin;
        }
    }
    public class RemoveCarCommandHandler : IRequestHandler<RemoveCarCommand, bool>
    {
        private readonly ICarRepository _carRepository;
        public RemoveCarCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<bool> Handle(RemoveCarCommand request, CancellationToken cancellationToken)
        {
            var result = await _carRepository.DeleteCarByVinNumber(request.VIN);

            return result;
        }
    }
}
