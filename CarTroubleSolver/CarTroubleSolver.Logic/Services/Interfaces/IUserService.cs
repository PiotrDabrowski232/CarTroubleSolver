using CarTroubleSolver.Logic.Dto.User;

namespace CarTroubleSolver.Logic.Services.Interfaces
{
    public interface IUserService
    {
        public UserDto RegisterUser(RegisterUserDto user);
        public UserDto? GetUser();
        public void UpdateUserData(UpdateUserDto user);
        public void ChangePassword(ChangePasswordUserDto user);
        public void DeleteUser(Guid id);
        public IEnumerable<UserDto> GetUsers();
        public string GenerateJwt(LoginDto login);
    }
}
