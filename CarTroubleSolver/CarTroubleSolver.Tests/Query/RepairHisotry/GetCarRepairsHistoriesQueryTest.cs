using CarTroubleSolver.Logic.Dto.Repairs;
using CarTroubleSolver.Logic.Functions.RepairHistory;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models;
using CarTroubleSolver.Shared.Models.Enum;
using CarTroubleSolver.Shared.Models.Enums.Models;
using CarTroubleSolver.Shared.Models.ExtraModels;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using CarTroubleSolver.Shared.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CarTroubleSolver.Tests.Query.RepairHistory
{
    public class GetCarRepairsHistoriesQueryTest
    {
        private readonly GetCarRepairsHitoriesQueryHandler _handler;
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly Mock<IStatusHistoryRepository> _statusHistoryRepositoryMock;

        public GetCarRepairsHistoriesQueryTest()
        {
            _dbContext = DbContextFactory.Create(); // Assume DbContextFactory creates an in-memory test DB context
            _statusHistoryRepositoryMock = new Mock<IStatusHistoryRepository>();
            _handler = new GetCarRepairsHitoriesQueryHandler(_dbContext, _statusHistoryRepositoryMock.Object);
        }

        [Fact]
        public async Task GetCarRepairsHistoriesQuery_ReturnsCorrectRepairHistory_WhenCarExists()
        {
            // Arrange
            var vin = "1HGCM82633A123456";

            var workshop = new Shared.Models.WorkshopPanel.Workshop
            {
                Id = Guid.NewGuid(),
                Name = "Test Workshop",
                PhoneNumber = 123456789,
                Email = "Workshop@o2.pl",
                Location = new NetTopologySuite.Geometries.Point(0, 0),
                Password = "Passwrod123!"
            };

            var car = new Shared.Models.UserPanel.Car
            {
                Id = Guid.NewGuid(),
                VIN = vin,
                Engine = "v8",
                Brand = Shared.Models.Enums.Brand.Toyota,
                Model = ToyotaModels.Tacoma.ToString(),
                RepairHistory = new List<Shared.Models.ExtraModels.RepairHistory>
                {
                    new Shared.Models.ExtraModels.RepairHistory
                    {
                        Id = Guid.NewGuid(),
                        WorkshopId = workshop.Id,
                        Workshop = workshop,
                        Service = ServiceType.MechanicalService,
                        Price = 250,
                        SpentHours = 3,
                        AccidentId = Guid.NewGuid(),
                        HistoryItems = new List<HistoryItems>
                        {
                            new HistoryItems { Name = "Brake Pad", Price = 100, Amount = 2 },
                            new HistoryItems { Name = "Oil Change", Price = 50, Amount = 1 }
                        }
                    }
                }
            };

            await _dbContext.Workshops.AddAsync(workshop);
            await _dbContext.Cars.AddAsync(car);
            await _dbContext.SaveChangesAsync();

            var accidentDate = DateTime.Now.AddDays(-30);
            _statusHistoryRepositoryMock
                .Setup(repo => repo.GetNewestAccidentStatus(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Guid id, CancellationToken token) => ("StatusExample", accidentDate, 0));

            var query = new GetCarRepairsHitoriesQuery(vin);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);

            var repairHistory = result[0];
            Assert.Equal(workshop.Id, repairHistory.WorkshopId);
            Assert.Equal("Test Workshop", repairHistory.WorkshopName);
            Assert.Equal("123456789", repairHistory.WorkshopNumber);
            Assert.Equal("MechanicalService", repairHistory.Service);
            Assert.Equal(250.0m, repairHistory.Price);
            Assert.Equal(3, repairHistory.SpentHours);
            Assert.Equal(accidentDate, repairHistory.Date);

            Assert.NotNull(repairHistory.HistoryItems);
            Assert.Equal(2, repairHistory.HistoryItems.Count);

            Assert.Equal("Brake Pad", repairHistory.HistoryItems[0].Name);
            Assert.Equal(100, repairHistory.HistoryItems[0].Price);
            Assert.Equal(2, repairHistory.HistoryItems[0].Quantity);

            Assert.Equal("Oil Change", repairHistory.HistoryItems[1].Name);
            Assert.Equal(50, repairHistory.HistoryItems[1].Price);
            Assert.Equal(1, repairHistory.HistoryItems[1].Quantity);
        }
    }
}
