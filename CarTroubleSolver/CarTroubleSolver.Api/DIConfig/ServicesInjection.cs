using CarTroubleSolver.Logic.Factories.Car.Model.ModelFactory;
using CarTroubleSolver.Logic.Services;
using CarTroubleSolver.Logic.Services.Interfaces;
using CarTroubleSolver.Shared.Auth;
using CarTroubleSolver.Shared.Models.ExtraModels;
using CarTroubleSolver.Shared.Services;
using CarTroubleSolver.Shared.Services.Interface;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace CarTroubleSolver.Api.DIConfig
{
    public static class ServicesInjection
    {
        public static IServiceCollection WithServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddScoped<IRoleService, RoleService>();
            Services.AddScoped<IUserService, UserService>();
            Services.AddScoped<ICarService, CarService>();
            Services.AddScoped<IFileService, FileService>();

            var assemblies = Assembly.Load("CarTroubleSolver.Logic");


            Services.Configure<AuthenticationSettings>(configuration.GetSection("Authentication"));

            Services.AddScoped<ITokenService, TokenService>();

            Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            Services.AddScoped<IPasswordHasher<Account>, PasswordHasher<Account>>();

            Services.AddScoped<IHashingService, HashingService>();

            Services.AddAutoMapper(assemblies);
            Services.AddSingleton<ModelFactory>();

            return Services;
        }
    }
}
