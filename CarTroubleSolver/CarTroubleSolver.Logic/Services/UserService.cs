using AutoMapper;
using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Data.Repositories.Interfaces;
using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Services
{
    public class UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher<User> passwordHasher) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;

        public void ChangePassword(ChangePasswordUserDto user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public UserDto? GetUser(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserDto> GetUsers()
        {
            throw new NotImplementedException();
        }

        public UserDto RegisterUser(RegisterUserDto user)
        {
            var mappedUser = _mapper.Map<User>(user);
            _userRepository.Add(mappedUser);

            var result = _mapper.Map<UserDto>(mappedUser);
            return result;
            
        }

        public void UpdateUserData(UpdateUserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
