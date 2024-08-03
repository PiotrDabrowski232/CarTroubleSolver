using CarTroubleSolver.Data.Repositories.Interfaces;
using MediatR;

namespace CarTroubleSolver.Logic.Functions.Car.Command
{
    public class RemoveCarCommand : IRequest
    {
        public string VIN { get; set; }

        public RemoveCarCommand(string vin)
        {
            VIN = vin;
        }
    }
    public class RemoveCarCommandHandler : IRequestHandler<RemoveCarCommand> 
    {
        private readonly ICarRepository _carRepository;
        public RemoveCarCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public Task Handle(RemoveCarCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();    
        }
    }
}
