using CarTroubleSolver.Workshop.Data.Repositories;
using CarTroubleSolver.Workshop.Data.Repositories.Interfaces;

namespace CarTroubleSolver.Workshop.Api.DIConfig
{
    public static class RepositoryInjection
    {
        public static IServiceCollection WithRepositories(this IServiceCollection Services)
        {
            Services.AddScoped<IWorkshopRepository, WorkshopRepository>();

            return Services;
        }
    }
}
