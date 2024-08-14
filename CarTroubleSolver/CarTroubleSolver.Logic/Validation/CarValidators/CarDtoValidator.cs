using CarTroubleSolver.Data.Data;
using CarTroubleSolver.Logic.Dto.Car;
using FluentValidation;

namespace CarTroubleSolver.Logic.Validation.CarValidators
{
    public class CarDtoValidator : AbstractValidator<CarDto>
    {
        public CarDtoValidator() { }
        public CarDtoValidator(CarTroubleSolverDbContext dbContext)
        {

            RuleFor(x => x.VIN)
                .Must(x => x.ToString().Length == 17)
                .WithMessage("VIN should have 17 digits")
                .Custom((value, context) =>
                {
                    var vin = dbContext.Cars.Any(u => u.VIN == value);

                    if (vin)
                        context.AddFailure("Vin", "Invalid Vin number");
                })
                .NotEmpty()
                .WithMessage("Fill Vin Input");

            RuleFor(x => x.DoorCount)
                .LessThanOrEqualTo(5)
                .WithMessage("Cars has max 5 doors");

            RuleFor(x => x.DateOfProduction)
                .NotEmpty().WithMessage("Date of production must not be empty")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Date of production must not be in the future");

            RuleFor(x => x.Model)
                .NotEmpty()
                .WithMessage("Select Model");

            RuleFor(x => x.Brand)
                .NotEmpty()
                .WithMessage("Select Brand");

            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Select Type");

            RuleFor(x => x.Mileage)
                .NotEmpty()
                .WithMessage("Mileage can not be empty");

            RuleFor(x => x.Engine)
                .NotEmpty()
                .WithMessage("Engine can not be empty");
        }
    }
}


