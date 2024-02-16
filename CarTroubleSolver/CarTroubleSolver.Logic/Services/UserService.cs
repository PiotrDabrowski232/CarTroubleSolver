using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Services
{
    public class UserService : IUserService
    {
        public void ChangePassword(ChangePasswordUserDto user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public RegisterUserDto GetUser(string username)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserDto> GetUsers()
        {
            throw new NotImplementedException();
        }

        public RegisterUserDto RegisterUser(RegisterUserDto user)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserData(UpdateUserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
