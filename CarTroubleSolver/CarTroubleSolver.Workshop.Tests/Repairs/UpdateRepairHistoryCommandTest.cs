using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.Enum;
using CarTroubleSolver.Workshop.Logic.Dto;
using CarTroubleSolver.Workshop.Logic.Functions.Repairs;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CarTroubleSolver.Workshop.Tests.Repairs
{
    public class UpdateRepairHistoryCommandTest
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly UpdateRepairHistoryCommandHandler _handler;

        public UpdateRepairHistoryCommandTest()
        {
            // Użycie InMemory bazy danych
            var options = new DbContextOptionsBuilder<CarTroubleSolverDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            // Tworzenie kontekstu bazy danych
            _dbContext = new CarTroubleSolverDbContext(options);

            // Tworzenie handlera z zależnością do DbContext
            _handler = new UpdateRepairHistoryCommandHandler(_dbContext);
        }

        [Fact]
        public async Task Handle_Should_Update_RepairHistory_And_HistoryItems()
        {
            // Arrange
            var accidentId = Guid.NewGuid().ToString();

            var existingRepairHistory = new Shared.Models.ExtraModels.RepairHistory
            {
                AccidentId = Guid.Parse(accidentId),
                Service = ServiceType.MechanicalService,
                Price = 500,
                SpentHours = 5,
                HistoryItems = new List<Shared.Models.ExtraModels.HistoryItems>
                {
                    new Shared.Models.ExtraModels.HistoryItems { Name = "Item1", Price = 100, Amount = 2 },
                    new Shared.Models.ExtraModels.HistoryItems { Name = "Item2", Price = 50, Amount = 1 }
                }
            };

            _dbContext.RepairHistory.Add(existingRepairHistory);
            await _dbContext.SaveChangesAsync();

            var updatedHistory = new RepairHistoryDto
            {
                Service = ServiceType.CarInspection.ToString(),
                Price = 600,
                SpentHours = 6,
                HistoryItems = new List<HistoryItemDto>
                {
                    new HistoryItemDto { Name = "Item3", Price = 200, Quantity = 3 },
                    new HistoryItemDto { Name = "Item4", Price = 80, Quantity = 2 }
                }
            };

            var command = new UpdateRepairHistoryCommand(accidentId, updatedHistory);

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            var updatedRepairHistory = await _dbContext.RepairHistory
                .Include(rh => rh.HistoryItems)
                .FirstOrDefaultAsync(rh => rh.AccidentId == Guid.Parse(accidentId));

            Assert.True(result);

            Assert.Equal(ServiceType.CarInspection, updatedRepairHistory.Service);
            Assert.Equal(600, updatedRepairHistory.Price);
            Assert.Equal(6, updatedRepairHistory.SpentHours);

            Assert.Equal(2, updatedRepairHistory.HistoryItems.Count);

            var historyItems = updatedRepairHistory.HistoryItems.ToList();
            Assert.Contains(historyItems, h => h.Name == "Item3" && h.Price == 200 && h.Amount == 3);
            Assert.Contains(historyItems, h => h.Name == "Item4" && h.Price == 80 && h.Amount == 2);
        }
    }
}
