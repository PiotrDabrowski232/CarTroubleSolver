using CarTroubleSolver.Data.Data;
using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Data.Repositories
{
    public class RoleRepository(CarTroubleSolverDbContext dbContext) : GenericRepository<Role>(dbContext), IGenericRepository<Role>, IRoleRepository
    {
    }
}
