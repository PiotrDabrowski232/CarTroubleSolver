using AutoMapper;
using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Services;
using CarTroubleSolver.Shared.Exceptions;
using CarTroubleSolver.Shared.Models.UserPanel;
using CarTroubleSolver.Shared.Repositories.Interfaces;
using CarTroubleSolver.Shared.Services.Interface;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace CarTroubleSolver.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHashingService> _hashingServiceMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _hashingServiceMock = new Mock<IHashingService>();
            _tokenServiceMock = new Mock<ITokenService>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            _userService = new UserService(
                _userRepositoryMock.Object,
                _mapperMock.Object,
                _hashingServiceMock.Object,
                _tokenServiceMock.Object,
                _httpContextAccessorMock.Object
            );
        }

        public static IEnumerable<object[]> RegisterUserData()
        {
            yield return new object[]
            {
            new RegisterUserDto
            {
                Name = "Test",
                Surname = "Test",
                Email = "test@example.com",
                Password = "Password123",
                PasswordConfirmed = "Password123",
                PhoneNumber = 123123123,
                DateOfBirth = new DateOnly(2000, 01, 19)
            },
            new User
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                Password = "HashedPassword",
                PhoneNumber = 123123123,
                DateOfBirth = new DateOnly(2000, 01, 19),
                Name = "Test",
                Surname = "Test"
            },
            new UserDto
            {
                Email = "test@example.com",
                Name = "Test",
                PhoneNumber = 123123123,
                DateOfBirth = new DateOnly(2000, 01, 19)
            }
            };
        }

        [Theory]
        [MemberData(nameof(RegisterUserData))]
        public void RegisterUser_ShouldReturnUserDto_WhenRegistrationIsSuccessful(
       RegisterUserDto registerUserDto,
       User user,
       UserDto expectedUserDto)
        {
            // Arrange
            _mapperMock.Setup(m => m.Map<User>(registerUserDto)).Returns(user);
            _hashingServiceMock.Setup(h => h.HashPassword(It.IsAny<User>(), registerUserDto.Password)).Returns("HashedPassword");
            _mapperMock.Setup(m => m.Map<UserDto>(user)).Returns(expectedUserDto);

            // Act
            var result = _userService.RegisterUser(registerUserDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUserDto.Email, result.Email);
            Assert.Equal(expectedUserDto.Name, result.Name);
            Assert.Equal(expectedUserDto.PhoneNumber, result.PhoneNumber);
            _userRepositoryMock.Verify(repo => repo.Add(user), Times.Once);
            _mapperMock.Verify(m => m.Map<UserDto>(user), Times.Once);
        }

        [Fact]
        public void GetUsers_WhenCalled_ShouldReturnAllMappedUsers()
        {

            // Arrange

            IQueryable<User> users = new List<User>()
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "test@example1.com",
                    Password = "HashedPassword",
                    PhoneNumber = 123123123,
                    DateOfBirth = new DateOnly(2000, 01, 19),
                    Name = "Piotr",
                    Surname = "Daborwski"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "test@example2.com",
                    Password = "HashedPassword",
                    PhoneNumber = 123123123,
                    DateOfBirth = new DateOnly(2000, 01, 19),
                    Name = "Piotr",
                    Surname = "Daborwski"
                },
            }.AsQueryable();

            IEnumerable<UserDto> expectedUsersDto = new List<UserDto>()
            {
                new UserDto
                {
                    Email = "test@example1.com",
                    PhoneNumber = 123123123,
                    DateOfBirth = new DateOnly(2000, 01, 19),
                    Name = "Piotr",
                    Surname = "Daborwski"
                },
                new UserDto
                {
                    Email = "test@example2.com",
                    PhoneNumber = 123123123,
                    DateOfBirth = new DateOnly(2000, 01, 19),
                    Name = "Piotr",
                    Surname = "Daborwski"
                },
            };

            _userRepositoryMock.Setup(repo => repo.GetAll()).Returns((IQueryable<User>)users);
            _mapperMock.Setup(m => m.Map<IEnumerable<UserDto>>(users)).Returns(expectedUsersDto);

            // Act
            var result = _userService.GetUsers();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUsersDto, result);
            _userRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<UserDto>>(users), Times.Once);
        }

        [Fact]
        public void GetUsers_WhenNoUsersExist_ShouldThrowNotFoundException()
        {
            // Arrange
            IQueryable<User> users = new List<User>().AsQueryable();

            _userRepositoryMock.Setup(repo => repo.GetAll()).Returns(users);
            _mapperMock.Setup(m => m.Map<IEnumerable<UserDto>>(users)).Returns(new List<UserDto>());

            // Act & Assert
            Assert.Throws<NotFoundException>(() => _userService.GetUsers());

            _userRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
            _mapperMock.Verify(m => m.Map<IEnumerable<UserDto>>(It.IsAny<IEnumerable<User>>()), Times.Never);
        }

        [Fact]
        public void ChangePassword_ThrowNotFoundException_WhenPasswordIncorrect()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@example1.com",
                Password = "HashedPassword",
                PhoneNumber = 123123123,
                DateOfBirth = new DateOnly(2000, 01, 19),
                Name = "Piotr",
                Surname = "Daborwsi"
            };

            var newPasswordDto = new ChangePasswordUserDto
            {
                Id = user.Id.ToString(),
                NewPassword = "NewPassword2",
                ConfirmedNewPassword = "NewPassword2",
                OldPassword = "OldPassword"
            };

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(con => con.HttpContext.User).Returns(claimsPrincipal);

            _userRepositoryMock.Setup(x => x.Get(user.Id)).ReturnsAsync(user);

            _hashingServiceMock.Setup(x => x.VerifyHashedPassword(user, user.Password, newPasswordDto.OldPassword)).Returns(false);

            // Act & Assert

            Assert.Throws<NotFoundException>(() => _userService.ChangePassword(newPasswordDto));

            // Assert
            _userRepositoryMock.Verify(x => x.UpdatePassword(user.Id, "HashedNewPassword"), Times.Never);
        }

        [Fact]
        public void ChangePassword_ChangedPassword()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@example1.com",
                Password = "HashedPassword",
                PhoneNumber = 123123123,
                DateOfBirth = new DateOnly(2000, 01, 19),
                Name = "Piotr",
                Surname = "Daborwsi"
            };

            var newPasswordDto = new ChangePasswordUserDto
            {
                Id = user.Id.ToString(),
                NewPassword = "NewPassword2",
                ConfirmedNewPassword = "NewPassword2",
                OldPassword = "OldPassword"
            };

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(con => con.HttpContext.User).Returns(claimsPrincipal);

            _userRepositoryMock.Setup(x => x.Get(user.Id)).ReturnsAsync(user);

            _hashingServiceMock.Setup(x => x.VerifyHashedPassword(user, user.Password, newPasswordDto.OldPassword)).Returns(true);

            _hashingServiceMock.Setup(x => x.HashPassword(null, newPasswordDto.NewPassword)).Returns("HashedNewPassword");

            _userRepositoryMock.Setup(x => x.UpdatePassword(user.Id, It.IsAny<string>()));

            // Act
            _userService.ChangePassword(newPasswordDto);

            // Assert
            _userRepositoryMock.Verify(x => x.UpdatePassword(user.Id, "HashedNewPassword"), Times.Once);
        }

        [Fact]
        public void DeleteUser_RemovedUserFromDb()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@example1.com",
                Password = "HashedPassword",
                PhoneNumber = 123123123,
                DateOfBirth = new DateOnly(2000, 01, 19),
                Name = "Piotr",
                Surname = "Daborwsi"
            };

            _userRepositoryMock.Setup(repo => repo.Remove(user.Id));

            // Act
            _userService.DeleteUser(user.Id);

            // Assert
            _userRepositoryMock.Verify(repo => repo.Remove(user.Id), Times.Once, "The Remove method should be called exactly once with the user's ID.");
        }

        [Fact]
        public void GenerateJwt_Returned_JwtTokken()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@example1.com",
                Password = "HashedPassword",
                PhoneNumber = 123123123,
                DateOfBirth = new DateOnly(2000, 01, 19),
                Name = "Piotr",
                Surname = "Daborwsi"
            };

            var Loginuser = new LoginDto
            {
                Email = "test@example1.com",
                Password = "HashedPassword",
            };

            var users = new List<User> { user }.AsQueryable();

            _userRepositoryMock.Setup(x => x.GetAll()).Returns(users);
            _userRepositoryMock.Setup(x => x.Get(user.Id)).ReturnsAsync(user);
            _hashingServiceMock.Setup(x => x.VerifyHashedPassword(user, user.Password, Loginuser.Password)).Returns(true);

            _tokenServiceMock.Setup(x => x.GenerateJwt(user.Id.ToString(), user.Name, user.Email)).Returns("mocked.jwt.token");

            //Act
            var result = _userService.GenerateJwt(Loginuser);

            //Assert
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            Assert.Equal("mocked.jwt.token", result);
        }
    }
}

