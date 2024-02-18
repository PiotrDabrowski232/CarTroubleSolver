using AutoMapper;
using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Data.Repositories.Interfaces;
using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Exceptions;
using CarTroubleSolver.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace CarTroubleSolver.Logic.Services
{
    public class UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;
        private readonly IPasswordHasher<User> _passwordHasher = passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings = authenticationSettings;

        #region Private
        private bool CheckPasswordCoretness(string providedPassword, Guid id)
        {
            var user = _userRepository.Get(id).Result;

            return (_passwordHasher.VerifyHashedPassword(user, user.Password, providedPassword) == PasswordVerificationResult.Failed) ? throw new NotFoundException() :  true;

        }

        private string HashPassword(string password)
        {
            string result = _passwordHasher.HashPassword(null, password);
            return result;
        }
        #endregion 

        public void ChangePassword(ChangePasswordUserDto user)
        {
            try
            {
                CheckPasswordCoretness(user.OldPassword, user.Id);
                _userRepository.UpdatePassword(user.Id, user.NewPassword);
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

        public UserDto? GetUser(Guid id)
        {
            try
            {
                var user = _userRepository.Get(id).Result;
                var userDto = user != null ? _mapper.Map<UserDto>(user) : throw new NotFoundException("There is no user with specified data");
                return userDto;
            }
            catch (Exception ex)
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

            var result = CheckPasswordCoretness(login.Password, user.Id);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes());
        }
    }
}
