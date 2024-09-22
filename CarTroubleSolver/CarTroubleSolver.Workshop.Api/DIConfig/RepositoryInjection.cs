using CarTroubleSolver.Shared.Repositories;
using CarTroubleSolver.Shared.Repositories.Interfaces;

namespace CarTroubleSolver.Workshop.Api.DIConfig
{
    public static class RepositoryInjection
    {
        public static IServiceCollection WithRepositories(this IServiceCollection Services)
        {
            Services.AddScoped<IWorkshopRepository, WorkshopRepository>();
            Services.AddScoped<IHourRepository, HourRepository>();

            return Services;
        }
    }
}
