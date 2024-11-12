using CarTroubleSolver.Logic.Functions.Car.Command;
using CarTroubleSolver.Shared.Models.Enums;
using CarTroubleSolver.Shared.Models.UserPanel;
using CarTroubleSolver.Shared.Repositories.Interfaces;
using Microsoft.AspNetCore.Rewrite;
using Moq;

namespace CarTroubleSolver.Tests.Commands.Car
{
    public class RemoveCarCommandTest
    {
        private readonly RemoveCarCommandHandler _handler;
        private readonly Mock<ICarRepository> _carRepo;

        public RemoveCarCommandTest()
        {
            _carRepo = new Mock<ICarRepository>();
            _handler = new RemoveCarCommandHandler(_carRepo.Object);
        }

        [Fact]
        public async Task RemoveCarCommand_RemovedCarFromDb()
        {
            //Arrange
            var car = new Shared.Models.UserPanel.Car
            {
                Id = Guid.NewGuid(),
                VIN = "1HGBH41JXMN109186",
                Brand = Brand.Toyota,
                Model = "Corolla",
                Color = new CarColor { Red = 120, Green = 120, Blue = 120 },
                DoorCount = 4,
                DateOfProduction = new DateTime(2020, 5, 10),
                CarType = CarType.Sedan,
                Mileage = 15000,
                Engine = "1.8L 4-cylinder"
            };

            _carRepo.Setup(x => x.Get(car.Id)).ReturnsAsync(car);
            _carRepo.Setup(x => x.DeleteCarByVinNumber(car.VIN)).ReturnsAsync(true);

            //Act

            var command = new RemoveCarCommand(car.VIN);
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.NotNull(result);
            Assert.True(result);

        }
    }
}
