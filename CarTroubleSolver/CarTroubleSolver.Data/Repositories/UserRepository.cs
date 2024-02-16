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
    public class UserRepository : GenericRepository<User>, IGenericRepository<User>, IUserRepository
    {
        public UserRepository(CarTroubleSolverDbContext dbContext) : base(dbContext) { }
    }
}
