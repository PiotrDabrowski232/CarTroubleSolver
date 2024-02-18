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
    public class UserRepository(CarTroubleSolverDbContext dbContext) : GenericRepository<User>(dbContext), IGenericRepository<User>, IUserRepository
    {
        public void UpdatePassword(Guid id, string password)
        {
            var user = _context.Users.Find(id);
            user.Password = password;

            _context.Update(user);
            _context.SaveChanges();
        }

    }
}
