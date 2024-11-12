using CarTroubleSolver.Logic.Functions.User.Querry;
using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;
using AutoMapper;
using CarTroubleSolver.Shared.Models.UserPanel;
using System.Security.Claims;
using CarTroubleSolver.Shared.Models.Enums.Models;

namespace CarTroubleSolver.Tests.Query.User
{
    public class GetUserInfoQueryTest
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetBasicCarConfigQueryHandler _handler;

        public GetUserInfoQueryTest()
        {
            var options = new DbContextOptionsBuilder<CarTroubleSolverDbContext>()
                .UseInMemoryDatabase(databaseName: "CarTroubleSolverTestDb")
                .Options;

            _dbContext = new CarTroubleSolverDbContext(options);
            _dbContext.Database.EnsureCreated();

            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _mapperMock = new Mock<IMapper>();

            _handler = new GetBasicCarConfigQueryHandler(_httpContextAccessorMock.Object, _dbContext, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnUserDto_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new Shared.Models.UserPanel.User
            {
                Id = userId,
                Email = "test@example.com",
                Name = "John",
                Surname = "Doe",
                Password = "Password123!",
                Cars = new List<Shared.Models.UserPanel.Car>
                {
                    new Shared.Models.UserPanel.Car { Brand = Shared.Models.Enums.Brand.BMW, Engine = "v8", Model = Enum.GetName(BMWModles.Series8), VIN="123312312dacaq1312gsd" , Color = new CarColor { Red = 120, Green = 130, Blue = 122 }},
                    new Shared.Models.UserPanel.Car { Brand = Shared.Models.Enums.Brand.Audi, Engine = "v8", Model = Enum.GetName(AudiModels.A8), VIN="123312312dacaq1312111" , Color = new CarColor { Red = 120, Green = 130, Blue = 122 }}
                }
            };

            var userDto = new UserDto
            {
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname
            };

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _httpContextAccessorMock.Setup(con => con.HttpContext.User).Returns(claimsPrincipal);

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            _mapperMock.Setup(m => m.Map<UserDto>(It.IsAny<Shared.Models.UserPanel.User>())).Returns(userDto);

            // Act
            var query = new GetUserInfoQuerry { Id = userId };
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userDto.Email, result.Email);
            Assert.Equal(userDto.Name, result.Name);
            Assert.Equal(userDto.Surname, result.Surname);

            var userInDb = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            Assert.NotNull(userInDb);
        }
    }
}
