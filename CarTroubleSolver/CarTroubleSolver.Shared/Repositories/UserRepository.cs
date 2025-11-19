using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.UserPanel;
using CarTroubleSolver.Shared.Repositories.Interfaces;

namespace CarTroubleSolver.Shared.Repositories
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
