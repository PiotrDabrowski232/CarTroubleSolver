using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using CarTroubleSolver.Shared.Models.UserPanel;
using CarTroubleSolver.Workshop.Logic.Functions.Accident;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using CarTroubleSolver.Shared.Models.ExtraModels;
using CarTroubleSolver.Shared.Repositories.Interfaces;

namespace CarTroubleSolver.Workshop.Tests.Accident
{
    public class GetAccidentFullInfoQueryTest
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly Mock<IStatusHistoryRepository> _statusHistoryMock;
        private readonly GetAccidentFullInfoQueryHandler _handler;

        public GetAccidentFullInfoQueryTest()
        {
            _dbContext = DbContextFactory.Create(); 

            _statusHistoryMock = new Mock<IStatusHistoryRepository>();
            _handler = new GetAccidentFullInfoQueryHandler(_dbContext, _statusHistoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_AccidentDetails_When_AccidentExists()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "user@o2.pl",
                Name = "User",
                PhoneNumber = 123123123,
                Surname = "Test",
                Password = "Test",
            };
            _dbContext.Users.Add(user);

            var workshop = new Shared.Models.WorkshopPanel.Workshop
            {
                Id = Guid.NewGuid(),
                Name = "Workshop A",
                Password = "Test",
                Email = "workshop@a.com",
                Location = new NetTopologySuite.Geometries.Point(0, 0)
            };
            _dbContext.Workshops.Add(workshop);

            var car = new Car
            {
                Id = Guid.NewGuid(),
                Brand = Shared.Models.Enums.Brand.Toyota,
                Model = "Corolla",
                OwnerId = user.Id,
                Engine = "v8",
                VIN = "123asd123asd123as"
            };
            _dbContext.Cars.Add(car);

            var accident = new Shared.Models.ExtraModels.Accident
            {
                Id = Guid.NewGuid(),
                CarId = car.Id,
                WorkshopId = workshop.Id,
                ProblemDescription = "Engine failure",
                Service = Shared.Models.Enum.ServiceType.MechanicalService
            };
            _dbContext.Accidents.Add(accident);

            var statusHistory = new StatusHistory
            {
                AccidentId = accident.Id,
                Status = Shared.Models.Enum.AccidentStatus.Progress,
                Date = DateTime.Now
            };
            _dbContext.StatusHistory.Add(statusHistory);

            _dbContext.SaveChanges();

            _statusHistoryMock.Setup(m => m.GetNewestAccidentStatus(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(("InProgress", DateTime.Now, 3));

            // Act
            var query = new GetAccidentFullInfoQuery(accident.Id.ToString());
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(accident.Id, result.Id);
            Assert.Equal("MechanicalService", result.Service);
            Assert.Equal("InProgress", result.Status);
            Assert.NotNull(result.Car);
            Assert.Equal(accident.Car.Owner.PhoneNumber.ToString(), result.UserContact);
        }

        [Fact]
        public async Task Handle_Should_Return_Null_When_AccidentNotFound()
        {
            // Arrange: No need to add data here since we will query a non-existing accident ID
            var invalidAccidentId = Guid.NewGuid().ToString();

            // Mock: Setup the status history mock to return some dummy status (not needed for this case)
            _statusHistoryMock.Setup(m => m.GetNewestAccidentStatus(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(("Not Found", DateTime.Now, 3));

            // Act: Query handler with invalid ID
            var query = new GetAccidentFullInfoQuery(invalidAccidentId);
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert: Result should be null because the accident does not exist
            Assert.Null(result);
        }
    }
}
