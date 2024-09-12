using CarTroubleSolver.Shared.Models;

namespace CarTroubleSolver.Shared.Services.Interface
{
    public interface IHashingService
    {
        string HashPassword(Account user, string password);
        bool VerifyHashedPassword(Account user, string hashedPassword, string providedPassword);
    }
}
