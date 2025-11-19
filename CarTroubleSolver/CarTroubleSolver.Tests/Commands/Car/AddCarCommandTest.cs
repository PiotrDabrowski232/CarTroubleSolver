using AutoMapper;
using CarTroubleSolver.Logic.Dto.Car;
using CarTroubleSolver.Logic.Functions.Car.Command;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.Enums;
using CarTroubleSolver.Shared.Models.UserPanel;
using CarTroubleSolver.Shared.Repositories.Interfaces;
using CarTroubleSolver.Tests;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

public class AddCarCommandTest
{
    private readonly AddCarCommandHandler _handler;
    private readonly Mock<ICarRepository> _mockCarRepo;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IHttpContextAccessor> _mockHttpContext;
    private readonly CarTroubleSolverDbContext _dbContext;

    public AddCarCommandTest()
    {
        _dbContext = DbContextFactory.Create();
        _mockCarRepo = new Mock<ICarRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockHttpContext = new Mock<IHttpContextAccessor>();
        _handler = new AddCarCommandHandler(_mockCarRepo.Object, _mockMapper.Object, _mockHttpContext.Object);
    }

    [Fact]
    public async Task AddCarCommand_ReturnedCarDto_WhenAddingIsCompletedSuccessfully()
    {
        // Arrange
        var userId = Guid.NewGuid();

        CarDto carDto = new CarDto
        {
            VIN = "1HGBH41JXMN109186",
            Brand = "Toyota",
            Model = "Corolla",
            Color = new CarTroubleSolver.Logic.Dto.Color(120, 120, 120),
            DoorCount = 4,
            DateOfProduction = new DateTime(2020, 5, 10),
            Type = "Sedan",
            Mileage = 15000,
            Engine = "1.8L 4-cylinder"
        };

        var car = new Car
        {
            VIN = carDto.VIN,
            Brand = Brand.Toyota,
            Model = carDto.Model,
            Color = new CarColor { Red = 120, Green = 120, Blue = 120 },
            DoorCount = carDto.DoorCount,
            DateOfProduction = carDto.DateOfProduction,
            CarType = CarType.Sedan,
            Mileage = carDto.Mileage,
            Engine = carDto.Engine,
            OwnerId = userId
        };

        _mockMapper.Setup(x => x.Map<Car>(It.IsAny<CarDto>())).Returns(car);
        _mockMapper.Setup(x => x.Map<CarDto>(It.IsAny<Car>())).Returns(carDto);

        var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
        var identity = new ClaimsIdentity(claims, "TestAuthType");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        _mockHttpContext.Setup(con => con.HttpContext.User).Returns(claimsPrincipal);

        _mockCarRepo.Setup(x => x.Add(It.IsAny<Car>())).ReturnsAsync(car);

        // Act
        var command = new AddCarCommand(carDto);
        var result = await _handler.Handle(command, default);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(carDto.VIN, result.VIN);
        Assert.Equal(carDto.Model, result.Model);
        Assert.Equal(carDto.Mileage, result.Mileage);
        Assert.Equal(carDto.Engine, result.Engine);

        _mockCarRepo.Verify(repo => repo.Add(It.IsAny<Car>()), Times.Once);
    }
}
