using CarTroubleSolver.Logic.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Services.Interfaces
{
    public interface IUserService
    {
        public RegisterUserDto RegisterUser(RegisterUserDto user);
        public RegisterUserDto GetUser(string username);
        public void UpdateUserData(UpdateUserDto user);
        public void ChangePassword(ChangePasswordUserDto user);
        public void DeleteUser(Guid id);
        public IQueryable<UserDto> GetUsers();

    }
}
