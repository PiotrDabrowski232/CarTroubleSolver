using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.ExtraModels;
using CarTroubleSolver.Workshop.Logic.Functions.Repairs;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Workshop.Tests.Repairs
{
    public class GetRepairHistoryQueryTest
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly GetRepairHistoryQueryHandler _handler;

        public GetRepairHistoryQueryTest()
        {
            _dbContext = DbContextFactory.Create();
            _handler = new GetRepairHistoryQueryHandler(_dbContext);
        }

        [Fact]
        public async Task Handle_Should_Return_RepairHistory()
        {
            // Arrange
            var accidentId = Guid.NewGuid().ToString();

            var repairHistory = new Shared.Models.ExtraModels.RepairHistory
            {
                Id = Guid.NewGuid(),
                AccidentId = Guid.Parse(accidentId),
                Service = Shared.Models.Enum.ServiceType.MechanicalService,
                Price = 500,
                SpentHours = 5
            };

            var historyItems = new List<Shared.Models.ExtraModels.HistoryItems>
                {
                    new Shared.Models.ExtraModels.HistoryItems { Name = "Item1", Price = 100, Amount = 2, RepairHistoryId = repairHistory.Id },
                    new Shared.Models.ExtraModels.HistoryItems   { Name = "Item2", Price = 50, Amount = 1, RepairHistoryId = repairHistory.Id }
                };
            repairHistory.HistoryItems = historyItems;

            _dbContext.RepairHistory.Add(repairHistory);
            await _dbContext.SaveChangesAsync();

            // Act
            var query = new GetRepairHistoryQuery(accidentId);
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(result); 
            Assert.Equal(Shared.Models.Enum.ServiceType.MechanicalService.ToString(), result.Service);
            Assert.Equal(500, result.Price); 
            Assert.Equal(5, result.SpentHours); 
            Assert.Equal(2, result.HistoryItems.Count); 
            Assert.Equal("Item1", result.HistoryItems[0].Name); 
            Assert.Equal(100, result.HistoryItems[0].Price); 
            Assert.Equal(2, result.HistoryItems[0].Quantity); 
        }

        [Fact]
        public async Task Handle_Should_Return_NullHistoryItems_When_No_HistoryItems()
        {
            // Arrange
            var accidentId = Guid.NewGuid().ToString();

            var repairHistory = new Shared.Models.ExtraModels.RepairHistory
            {
                AccidentId = Guid.Parse(accidentId),
                Service = Shared.Models.Enum.ServiceType.MechanicalService,
                Price = 300,
                SpentHours = 3
            };

            _dbContext.RepairHistory.Add(repairHistory);
            await _dbContext.SaveChangesAsync();

            // Act
            var query = new GetRepairHistoryQuery(accidentId);
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(result); 
            Assert.Null(result.HistoryItems); 
        }
    }
}
