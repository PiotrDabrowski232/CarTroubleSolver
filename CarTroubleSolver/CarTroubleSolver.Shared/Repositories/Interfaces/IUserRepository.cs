using CarTroubleSolver.Shared.Models.UserPanel;

namespace CarTroubleSolver.Shared.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public void UpdatePassword(Guid id, string password);
    }
}
