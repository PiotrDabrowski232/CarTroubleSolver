using AutoMapper;
using CarTroubleSolver.Logic.Dto;
using CarTroubleSolver.Logic.Dto.Car;
using CarTroubleSolver.Logic.Functions.Car.Query;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.Enums.Models;
using CarTroubleSolver.Shared.Models.UserPanel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;

namespace CarTroubleSolver.Tests.Query.Car
{
    public class GetUserCarsQueryTest
    {
        private readonly GetUserCarsQueryHandler _handler;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IHttpContextAccessor> _mockHttpClient;
        private readonly CarTroubleSolverDbContext _dbContext;


        public GetUserCarsQueryTest()
        {
            var options = new DbContextOptionsBuilder<CarTroubleSolverDbContext>()
                .UseInMemoryDatabase(databaseName: "CarTroubleSolverTestDb")
                .Options;

            _dbContext = new CarTroubleSolverDbContext(options);
            _dbContext.Database.EnsureCreated();

            _mockMapper = new Mock<IMapper>();
            _mockHttpClient = new Mock<IHttpContextAccessor>();
            _handler = new GetUserCarsQueryHandler(_dbContext, _mockMapper.Object, _mockHttpClient.Object);
        }

        [Fact]
        public async Task GetUserCarsQuery_Returned_UserCars()
        {
            //Arrange
            var user = new Shared.Models.UserPanel.User
            {
                Id = Guid.NewGuid(),
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

            var cars = new List<CarBasicInfoDto>()
            {
                new CarBasicInfoDto{Brand = Shared.Models.Enums.Brand.BMW.ToString(), Model = Enum.GetName(BMWModles.Series8), VIN="123312312dacaq1312gsd" , Color = new Color( 120, 130,122 )},
                new CarBasicInfoDto{Brand = Shared.Models.Enums.Brand.Audi.ToString(), Model = Enum.GetName(AudiModels.A8), VIN="123312312dacaq1312111" , Color = new Color( 120, 130,122 )}
            };

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _mockHttpClient.Setup(con => con.HttpContext.User).Returns(claimsPrincipal);
            _dbContext.Add(user);
            await _dbContext.SaveChangesAsync();

            _mockMapper.Setup(x => x.Map<List<CarBasicInfoDto>>(It.IsAny<List<Shared.Models.UserPanel.Car>>())).Returns(cars);

            //Act
            var query = new GetUserCarsQuery();
            var result = await _handler.Handle(query, default);

            //Assert
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            Assert.Equal(result.Count, user.Cars.Count);
        }
    }
}
