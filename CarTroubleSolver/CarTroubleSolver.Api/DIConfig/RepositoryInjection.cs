using CarTroubleSolver.Data.Repositories.Interfaces;
using CarTroubleSolver.Data.Repositories;

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
