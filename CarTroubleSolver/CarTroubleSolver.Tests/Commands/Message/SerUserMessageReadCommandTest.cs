using CarTroubleSolver.Logic.Functions.Message;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CarTroubleSolver.Tests.Commands.Message
{
    public class SetUserMessageReadCommandTest
    {
        private readonly SetUserMessageReadCommandHandler _handler;
        private readonly CarTroubleSolverDbContext _dbContext;

        public SetUserMessageReadCommandTest()
        {
            _dbContext = DbContextFactory.Create(); 
            _handler = new SetUserMessageReadCommandHandler(_dbContext);
        }

        [Fact]
        public async Task SetUserMessageReadCommand_SetsIsReadToTrue_WhenMessageExists()
        {
            // Arrange
            var messageId = Guid.NewGuid();

            var message = new Shared.Models.ExtraModels.Message
            {
                Id = messageId,
                Content = "Sample message content",
                IsRead = false
            };

            await _dbContext.Messages.AddAsync(message);
            await _dbContext.SaveChangesAsync();

            var command = new SetUserMessageReadCommand(messageId.ToString());

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);

            var updatedMessage = await _dbContext.Messages.FirstOrDefaultAsync(x => x.Id == messageId);
            Assert.NotNull(updatedMessage);
            Assert.True(updatedMessage.IsRead);
        }
    }
}
