using CarTroubleSolver.Logic.Dto.Car;
using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Validation.CarValidators;
using CarTroubleSolver.Logic.Validation.UserValidators;
using FluentValidation;

namespace CarTroubleSolver.Api.DIConfig
{
    public static class ValidatorInjection
    {
        public static IServiceCollection WithValidation(this IServiceCollection Services)
        {
            Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            Services.AddScoped<IValidator<ChangePasswordUserDto>, ChangePasswordUserDtoValidator>();
            Services.AddScoped<IValidator<CarDto>, CarDtoValidator>();
            Services.AddScoped<IValidator<UpdateCarDto>, UpdateCarDtoValidator>();
            
            return Services;
        }
    }
}
