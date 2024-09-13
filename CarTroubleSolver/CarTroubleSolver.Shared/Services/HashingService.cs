using CarTroubleSolver.Shared.Models;
using CarTroubleSolver.Shared.Services.Interface;
using Microsoft.AspNetCore.Identity;

namespace CarTroubleSolver.Shared.Services
{
    public class HashingService : IHashingService
    {
        private readonly IPasswordHasher<Account> _passwordHasher;

        public HashingService(IPasswordHasher<Account> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public string HashPassword(Account user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        public bool VerifyHashedPassword(Account user, string hashedPassword, string providedPassword)
        {
             var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
             return result == PasswordVerificationResult.Success;
        }
    }
}
