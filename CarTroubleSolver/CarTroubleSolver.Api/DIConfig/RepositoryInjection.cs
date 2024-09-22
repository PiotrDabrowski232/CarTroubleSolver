using CarTroubleSolver.Shared.Repositories;
using CarTroubleSolver.Shared.Repositories.Interfaces;

namespace CarTroubleSolver.Api.DIConfig
{
    public static class RepositoryInjection
    {
        public static IServiceCollection WithRepositories(this IServiceCollection Services)
        {
            Services.AddScoped<IUserRepository, UserRepository>();
            Services.AddScoped<IRoleRepository, RoleRepository>();
            Services.AddScoped<ICarRepository, CarRepository>();

            return Services;
        }
    }
}
