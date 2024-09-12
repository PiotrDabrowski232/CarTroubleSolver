﻿using CarTroubleSolver.Shared.Services;
using CarTroubleSolver.Shared.Services.Interface;
using System.Reflection;

namespace CarTroubleSolver.Workshop.Api.DIConfig
{
    public static class ServicesInjection
    {
        public static IServiceCollection WithServices(this IServiceCollection Services)
        {

            var assemblies = Assembly.Load("CarTroubleSolver.Workshop.Logic");

            Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assemblies));

            Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            Services.AddSingleton<IGeoLocalizationService, GeoLocalizationService>();

            Services.AddScoped<IHashingService, HashingService>();

            Services.AddHttpClient();

            return Services;
        }
    }
}
