using CarTroubleSolver.Shared.Auth;
using CarTroubleSolver.Shared.Models.ExtraModels;
using CarTroubleSolver.Shared.Services;
using CarTroubleSolver.Shared.Services.Interface;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

namespace CarTroubleSolver.Workshop.Api.DIConfig
{
    public static class ServicesInjection
    {
        public static IServiceCollection WithServices(this IServiceCollection Services, IConfiguration configuration)
        {

            var assemblies = Assembly.Load("CarTroubleSolver.Workshop.Logic");

            Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assemblies));

            Services.Configure<AuthenticationSettings>(configuration.GetSection("Authentication"));

            Services.AddScoped<ITokenService, TokenService>();

            Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            Services.AddScoped<IPasswordHasher<Account>, PasswordHasher<Account>>();

            Services.AddScoped<IHashingService, HashingService>();

            Services.AddSingleton<IGeoLocalizationService, GeoLocalizationService>();

            Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            Services.AddHttpClient();

            return Services;
        }
    }
}
