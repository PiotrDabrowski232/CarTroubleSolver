using CarTroubleSolver.Data.Models.Enums;
using CarTroubleSolver.Logic.Dto.Car;
using CarTroubleSolver.Logic.Functions.Car.Query;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Functions.Car.Command
{
    public class AddCarCommand : IRequest<CarDto>
    {
        public CarDto Car { get; set; }
        public AddCarCommand() { }
        public AddCarCommand(CarDto car) {  Car = car; }
    }

    public class AddCarCommandHandler : IRequestHandler<AddCarCommand, CarDto>
    {
        public Task<CarDto> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
