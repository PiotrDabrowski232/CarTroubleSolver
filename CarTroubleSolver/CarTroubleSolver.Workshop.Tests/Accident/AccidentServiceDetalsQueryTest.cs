using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using CarTroubleSolver.Workshop.Logic.Functions.Accident;
using CarTroubleSolver.Workshop.Logic.Dto.Service;
using CarTroubleSolver.Shared.Data;
using Microsoft.EntityFrameworkCore;
using CarTroubleSolver.Shared.Models.Enum;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using CarTroubleSolver.Shared.Models.UserPanel;
using CarTroubleSolver.Shared.Models.ExtraModels;

namespace CarTroubleSolver.Workshop.Tests.Accident
{
    public class AccidentServiceDetailsQueryTest
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly AccidentServiceDetailsQueryHandler _handler;

        public AccidentServiceDetailsQueryTest()
        {
            _dbContext = DbContextFactory.Create();
            _handler = new AccidentServiceDetailsQueryHandler(_dbContext);
        }

        [Fact]
        public async Task Handle_Should_Return_ServiceDetails_When_AccidentExists()
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

            var service = new WorkshopServices
            {
                Id = Guid.NewGuid(),
                Service = Shared.Models.Enum.ServiceType.MechanicalService,
                Price = 120,
                WorkshopId = workshop.Id,
            };
            _dbContext.WorkshopServices.Add(service);
            _dbContext.SaveChanges();

            // Act
            var query = new AccidentServiceDetailsQuery(accident.Id.ToString());
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(120, result.Price);
            Assert.Equal(Enum.GetName(ServiceType.MechanicalService), result.ServiceType);
        }
    }
}
