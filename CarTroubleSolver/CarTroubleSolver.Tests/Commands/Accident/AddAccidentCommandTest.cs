using CarTroubleSolver.Logic.Functions.Accident;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models;
using CarTroubleSolver.Shared.Models.ExtraModels;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CarTroubleSolver.Tests.Commands.Accident
{
    public class AddAccidentCommandTest
    {
        private readonly AddAccidentCommandHandler _handler;
        private readonly CarTroubleSolverDbContext _dbcontext;

        public AddAccidentCommandTest()
        {
            _dbcontext = DbContextFactory.Create(); // Assuming DbContextFactory creates an in-memory test DB context
            _handler = new AddAccidentCommandHandler(_dbcontext);
        }

        [Fact]
        public async Task AddAccidentCommand_ReturnsTrue_WhenAccepted()
        {
            // Arrange
            var messageId = Guid.NewGuid().ToString();
            var workshopId = Guid.NewGuid();
            var carId = Guid.NewGuid();
            var dateOfVisit = DateTime.Now;

            var message = new Shared.Models.ExtraModels.Message
            {
                Id = Guid.Parse(messageId),
                SenderWorkshopId = workshopId,
                CarId = carId,
                DateOfVisit = dateOfVisit,
                PreviousMessage = new Shared.Models.ExtraModels.Message { Content = "Test problem description" },
                Content = "Message"
            };

            var workshopHours = new Shared.Models.WorkshopPanel.HourConfiguration
            {
                WorkshopId = workshopId,
                DayOfWeek = dateOfVisit.DayOfWeek,
                From =  TimeOnly.FromDateTime(dateOfVisit.AddHours(1)),
                To = TimeOnly.FromDateTime(dateOfVisit.AddHours(8))
            };

            await _dbcontext.Messages.AddAsync(message);
            await _dbcontext.Hours.AddAsync(workshopHours);
            await _dbcontext.SaveChangesAsync();

            //Act

            var command = new AddAccidentCommand(messageId, isAccepted: true);

            var result = await _handler.Handle(command, CancellationToken.None);


            //Assert
            Assert.True(result);

            var accident = await _dbcontext.Accidents.FirstOrDefaultAsync(x => x.CarId == carId);
            Assert.NotNull(accident);
            Assert.Equal(message.Service, accident.Service);
            Assert.Equal(message.SenderWorkshopId, accident.WorkshopId);

            var status = await _dbcontext.StatusHistory.FirstOrDefaultAsync(x => x.AccidentId == accident.Id);
            Assert.NotNull(status);
            Assert.Equal(Shared.Models.Enum.AccidentStatus.WaitingForCar, status.Status);

            var updatedMessage = await _dbcontext.Messages.FirstOrDefaultAsync(x => x.Id == Guid.Parse(messageId));
            Assert.True(updatedMessage.Responsed);
        }
    }
}
