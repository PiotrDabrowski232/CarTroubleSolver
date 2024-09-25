using CarTroubleSolver.Shared.Models;
using CarTroubleSolver.Workshop.Logic.Dto.Workshop;
using CarTroubleSolver.Workshop.Logic.Validation;
using FluentValidation;

namespace CarTroubleSolver.Workshop.Api.DIConfig
{
    public static class ValidatorInjection
    {
        public static IServiceCollection WithValidation(this IServiceCollection Services)
        {
            Services.AddScoped<IValidator<RegisterWorkshopDto>, RegisterWorkshopDtoValidation>();
            Services.AddScoped<IValidator<NewPasswordDto>, NewPasswordDtoValidation>();

            return Services;
        }
    }
}
