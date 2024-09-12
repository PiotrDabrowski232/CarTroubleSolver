using AutoMapper;
using CarTroubleSolver.Api.Authentication;
using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Data.Repositories.Interfaces;
using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Exceptions;
using CarTroubleSolver.Logic.Services.Interfaces;
using CarTroubleSolver.Shared.Services;
using CarTroubleSolver.Shared.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarTroubleSolver.Logic.Services
{
    public class UserService(IUserRepository userRepository, IRoleService roleService, IMapper mapper, IHashingService hashingService, ITokenService tokenService, IHttpContextAccessor httpContextAccessor) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IRoleService _roleService = roleService;
        private readonly IMapper _mapper = mapper;
        private readonly IHashingService _hashingService = hashingService;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly ITokenService _tokenService = tokenService;


        #region Private
        private bool CheckPasswordCoretness(string providedPassword, Guid id)
        {
            var user = _userRepository.Get(id).Result;

            return (_hashingService.VerifyHashedPassword(user, user.Password, providedPassword)) ? true : throw new NotFoundException("Incorrect Password") ;

        }

        private string HashPassword(string password)
        {
            string result = _hashingService.HashPassword(null, password);
            return result;
        }
        private User UserData(Guid Id)
        {
            return _userRepository.Get(Id).Result;
        }
        #endregion 

        public void ChangePassword(ChangePasswordUserDto user)
        {
            try
            {
                user.Id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                CheckPasswordCoretness(user.OldPassword, Guid.Parse(user.Id));
                user.NewPassword = HashPassword(user.NewPassword);
                _userRepository.UpdatePassword(Guid.Parse(user.Id), user.NewPassword);
            }
            catch
            {
                throw;
            }
        }

        public void DeleteUser(Guid id)
        {
            try
            {
                _userRepository.Remove(id);
            }
            catch
            {
                throw;
            }
        }

        public UserDto? GetUser()
        {
            try
            {
                if (_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) != null)
                {
                    var user = UserData(Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                    var userDto = user != null ? _mapper.Map<UserDto>(user) : throw new NotFoundException("There is no user with specified data");
                    return userDto;
                }
                else
                {
                    throw new NotFoundException("There is no user with specified data");
                }

            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<UserDto> GetUsers()
        {
            try
            {
                var users = _userRepository.GetAll();
                var usersDto = users.Any() ? _mapper.Map<IEnumerable<UserDto>>(users) : throw new NotFoundException("There is no users with specified data");
                return usersDto;
            }
            catch
            {
                throw;
            }
        }

        public UserDto RegisterUser(RegisterUserDto user)
        {
            try
            {
                var mappedUser = _mapper.Map<User>(user);
                mappedUser.Password = HashPassword(user.Password);
                mappedUser.Id = Guid.NewGuid();
                _userRepository.Add(mappedUser);

                var result = _mapper.Map<UserDto>(mappedUser);
                return result;
            }
            catch
            {
                throw;
            }
        }

        public void UpdateUserData(UpdateUserDto updateUser)
        {
            throw new NotImplementedException();
        }

        public string GenerateJwt(LoginDto login)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Email == login.Email);

            if (user == null)
            {
                throw new NotFoundException("Email or Password is wrong");
            }

            user.Role = _roleService.GetRole(user.RoleId);

            CheckPasswordCoretness(login.Password, user.Id);

            return _tokenService.GenerateJwt(user.Id.ToString(), user.Name, user.Email);
        }
    }
}
