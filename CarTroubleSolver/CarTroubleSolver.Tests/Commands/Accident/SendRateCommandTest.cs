using CarTroubleSolver.Logic.Dto;
using CarTroubleSolver.Logic.Functions.Accident;
using CarTroubleSolver.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Tests.Commands.Accident
{
    public class SendRateCommandTest
    {
        private readonly SendRateCommandHandler _handler;
        private readonly CarTroubleSolverDbContext _dbcontext;

        public SendRateCommandTest()
        {
            _dbcontext = DbContextFactory.Create(); // Assume an in-memory test DB context
            _handler = new SendRateCommandHandler(_dbcontext);
        }

        [Fact]
        public async Task SendRateCommand_ReturnsTrue_WhenSuccessfullySavesRating()
        {
            // Arrange
            var messageId = Guid.NewGuid().ToString();
            var receiverUserId = Guid.NewGuid();
            var senderWorkshopId = Guid.NewGuid();

            var message = new Shared.Models.ExtraModels.Message
            {
                Id = Guid.Parse(messageId),
                ReceiverUserId = receiverUserId,
                SenderWorkshopId = senderWorkshopId,
                Responsed = false,
                Content = "Message"
            };

            await _dbcontext.Messages.AddAsync(message);
            await _dbcontext.SaveChangesAsync();

            var rateDto = new RateDto
            {
                Comment = "Excellent service!",
                Rate = 5
            };

            var command = new SendRateCommand(messageId, rateDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);

            var rating = await _dbcontext.Rating.FirstOrDefaultAsync(r => r.WorkshopId == senderWorkshopId);
            Assert.NotNull(rating);
            Assert.Equal(rateDto.Comment, rating.Comment);
            Assert.Equal(rateDto.Rate, rating.Rate);
            Assert.Equal(receiverUserId, rating.UserId);
            Assert.Equal(senderWorkshopId, rating.WorkshopId);

            var updatedMessage = await _dbcontext.Messages.FirstOrDefaultAsync(m => m.Id == Guid.Parse(messageId));
            Assert.True(updatedMessage.Responsed);
        }
    }
}
