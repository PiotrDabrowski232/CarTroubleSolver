using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Logic.Services.Interfaces;
using CarTroubleSolver.Logic.Services;
using Microsoft.AspNetCore.Identity;
using CarTroubleSolver.Logic.Factories.Car.Model.ModelFactory;

namespace CarTroubleSolver.Api.DIConfig
{
    public static class ServicesInjection
    {
        public static IServiceCollection WithServices(this IServiceCollection Services)
        {
            Services.AddScoped<IRoleService, RoleService>();
            Services.AddScoped<IUserService, UserService>();
            Services.AddScoped<ICarService, CarService>();
            Services.AddScoped<IFileService, FileService>();

            Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            Services.AddSingleton<ModelFactory>();

            return Services;
        }
    }
}
