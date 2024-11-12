using CarTroubleSolver.Logic.Dto;
using CarTroubleSolver.Logic.Functions.StausHistory;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models;
using CarTroubleSolver.Shared.Models.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CarTroubleSolver.Tests.Query.StatusHistory
{
    public class GetStatusHistoryQueryTest
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly GetStatusHistoryQueryHandler _handler;

        public GetStatusHistoryQueryTest()
        {
            // Setting up in-memory database context for testing
            var options = new DbContextOptionsBuilder<CarTroubleSolverDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new CarTroubleSolverDbContext(options);
            _handler = new GetStatusHistoryQueryHandler(_dbContext);
        }

        [Fact]
        public async Task GetStatusHistoryQuery_ReturnsCorrectStatusHistory_ForGivenAccidentId()
        {
            // Arrange
            var accidentId = Guid.NewGuid();

            var statusHistoryRecords = new List<Shared.Models.ExtraModels.StatusHistory>
            {
                new Shared.Models.ExtraModels.StatusHistory
                {
                    Id = Guid.NewGuid(),
                    AccidentId = accidentId,
                    Status = AccidentStatus.WaitingForCar,
                    Date = DateTime.Now.AddDays(-2),
                    ControlQueue = 1
                },
                new Shared.Models.ExtraModels.StatusHistory
                {
                    Id = Guid.NewGuid(),
                    AccidentId = accidentId,
                    Status = AccidentStatus.Progress,
                    Date = DateTime.Now.AddDays(-1),
                    ControlQueue = 2
                },
                new Shared.Models.ExtraModels.StatusHistory
                {
                    Id = Guid.NewGuid(),
                    AccidentId = accidentId,
                    Status = AccidentStatus.ReadyToReceive,
                    Date = DateTime.Now,
                    ControlQueue = 3
                }
            };

            await _dbContext.StatusHistory.AddRangeAsync(statusHistoryRecords);
            await _dbContext.SaveChangesAsync();

            var query = new GetStatusHistoryQuery(accidentId.ToString());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            Assert.Equal("WaitingForCar", result[0].Status);
            Assert.Equal("Progress", result[1].Status);
            Assert.Equal("ReadyToReceive", result[2].Status);
            Assert.True(result[0].Date < result[1].Date && result[1].Date < result[2].Date);
        }
    }
}
