using CarTroubleSolver.Logic.Dto.Message;
using CarTroubleSolver.Logic.Functions.Message;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models;
using CarTroubleSolver.Shared.Models.Enum;
using CarTroubleSolver.Shared.Models.Enums;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CarTroubleSolver.Tests.Commands.Message
{
    public class ReceiveMessageDetailsQueryTest
    {
        private readonly ReceiveMessageDetailsQueryHandler _handler;
        private readonly CarTroubleSolverDbContext _dbContext;

        public ReceiveMessageDetailsQueryTest()
        {
            _dbContext = DbContextFactory.Create(); 
            _handler = new ReceiveMessageDetailsQueryHandler(_dbContext);
        }

        [Fact]
        public async Task ReceiveMessageDetailsQuery_ReturnsCorrectMessageDetails_WhenMessageExists()
        {
            // Arrange: 
            var messageId = Guid.NewGuid();
            var workshopId = Guid.NewGuid();
            var carId = Guid.NewGuid();

            var workshop = new Workshop
            {
                Id = workshopId,
                Name = "Test Workshop",
                Email="Workshop@o2.pl",
                Location = new NetTopologySuite.Geometries.Point(0, 0),
                Password = "Passwrod123!"
            };

            var car = new Shared.Models.UserPanel.Car
            {
                Id = carId,
                VIN = "1HGCM82633A123456",
                Brand = Brand.Toyota,
                Model = "Camry",
                Engine = "2.4L",
                Mileage = 123456
            };

            var message = new Shared.Models.ExtraModels.Message
            {
                Id = messageId,
                Content = "This is a test message.",
                SenderWorkshopId = workshopId,
                SenderWorkshop = workshop,
                IsRead = true,
                SentAt = DateTime.Now.AddDays(-1),
                DateOfVisit = DateTime.Now.AddDays(1),
                Service = ServiceType.MechanicalService,
                RateMessage = false,
                CarId = carId,
                Car = car
            };

            await _dbContext.Workshops.AddAsync(workshop);
            await _dbContext.Cars.AddAsync(car);
            await _dbContext.Messages.AddAsync(message);
            await _dbContext.SaveChangesAsync();

            var query = new ReceiveMessageDetailsQuery(messageId.ToString());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(message.Id, result.Id);
            Assert.Equal(message.Content, result.Message);
            Assert.Equal(workshop.Name, result.WorkshopName);
            Assert.Equal(message.IsRead, result.IsRead);
            Assert.Equal(message.SentAt, result.SentAt);
            Assert.Equal(message.DateOfVisit, result.DateOfVisit);
            Assert.Equal(Enum.GetName(typeof(ServiceType), message.Service), result.Service);
            Assert.Equal(message.RateMessage, result.RateMessage);

            Assert.NotNull(result.Car);
            Assert.Equal(car.VIN.ToUpper(), result.Car.VIN);
            Assert.Equal(Enum.GetName(typeof(Brand), car.Brand), result.Car.Brand);
            Assert.Equal(car.Model, result.Car.Model);
            Assert.Equal(car.Engine, result.Car.Engine);
            Assert.Equal(car.Mileage, result.Car.Mileage);
        }
    }
}
