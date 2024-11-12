using CarTroubleSolver.Logic.Dto.Message;
using CarTroubleSolver.Logic.Functions.Message;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using CarTroubleSolver.Shared.Models.Enum;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using CarTroubleSolver.Shared.Models.Enums;

namespace CarTroubleSolver.Tests.Query.Message
{
    public class ReceiveMessageDetailsQueryTest
    {
        private readonly ReceiveMessageDetailsQueryHandler _handler;
        private readonly CarTroubleSolverDbContext _dbContext;

        public ReceiveMessageDetailsQueryTest()
        {
            _dbContext = DbContextFactory.Create(); // Assume DbContextFactory creates an in-memory test DB context
            _handler = new ReceiveMessageDetailsQueryHandler(_dbContext);
        }

        [Fact]
        public async Task ReceiveMessageDetailsQuery_ReturnsCorrectMessageDetails_WhenMessageExists()
        {
            // Arrange
            var workshop = new Shared.Models.WorkshopPanel.Workshop
            {
                Id = Guid.NewGuid(),
                Name = "Test Workshop",
                Email = "Workshop@o2.pl",
                Location = new NetTopologySuite.Geometries.Point(0, 0),
                Password = "Passwrod123!"
            };

            var car = new Shared.Models.UserPanel.Car
            {
                Id = Guid.NewGuid(),
                VIN = "1HGCM82633A123456",
                Brand = Brand.Toyota,
                Model = "Camry",
                Engine = "2.4L",
                Mileage = 120000
            };

            var messageId = Guid.NewGuid();
            var message = new Shared.Models.ExtraModels.Message
            {
                Id = messageId,
                Content = "Test message content",
                SenderWorkshopId = workshop.Id,
                SenderWorkshop = workshop,
                CarId = car.Id,
                Car = car,
                IsRead = false,
                SentAt = DateTime.Now.AddDays(-1),
                DateOfVisit = DateTime.Now.AddDays(1),
                Service = ServiceType.MechanicalService,
                RateMessage = true
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
            Assert.Equal("Test message content", result.Message);
            Assert.Equal("Test Workshop", result.WorkshopName);
            Assert.False(result.IsRead);
            Assert.Equal(message.SentAt, result.SentAt);
            Assert.Equal(message.DateOfVisit, result.DateOfVisit);
            Assert.Equal("MechanicalService", result.Service);
            Assert.Equal(true, result.RateMessage);

            
            Assert.NotNull(result.Car);
            Assert.Equal("1HGCM82633A123456", result.Car.VIN);
            Assert.Equal("Toyota", result.Car.Brand);
            Assert.Equal("Camry", result.Car.Model);
            Assert.Equal("2.4L", result.Car.Engine);
            Assert.Equal(120000, result.Car.Mileage);
        }
    }
}
