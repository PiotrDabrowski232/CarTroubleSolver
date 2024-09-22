using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.UserPanel;
using CarTroubleSolver.Shared.Repositories.Interfaces;

namespace CarTroubleSolver.Shared.Repositories
{
    public class RoleRepository(CarTroubleSolverDbContext dbContext) : GenericRepository<Role>(dbContext), IGenericRepository<Role>, IRoleRepository
    {
    }
}
