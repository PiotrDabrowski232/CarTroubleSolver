using CarTroubleSolver.Data.Data;
using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Data.Repositories.Interfaces;

namespace CarTroubleSolver.Data.Repositories
{
    public class RoleRepository(CarTroubleSolverDbContext dbContext) : GenericRepository<Role>(dbContext), IGenericRepository<Role>, IRoleRepository
    {
    }
}
