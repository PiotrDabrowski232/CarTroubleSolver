using CarTroubleSolver.Logic.Dto.Message;
using CarTroubleSolver.Logic.Functions.Message;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models;
using CarTroubleSolver.Shared.Models.Enum;
using CarTroubleSolver.Shared.Models.Enums;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Security.Claims;
using Xunit;

namespace CarTroubleSolver.Tests.Query.Message
{
    public class ReceiveMessageQueryTest
    {
        private readonly ReceiveMessageQueryHandler _handler;
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        public ReceiveMessageQueryTest()
        {
            _dbContext = DbContextFactory.Create(); 
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var userId = Guid.NewGuid().ToString();
            var claims = new[] { new Claim(ClaimTypes.NameIdentifier, userId) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            var httpContext = new DefaultHttpContext { User = claimsPrincipal };
            _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContext);

            _handler = new ReceiveMessageQueryHandler(_httpContextAccessorMock.Object, _dbContext);
        }

        [Fact]
        public async Task ReceiveMessageQuery_ReturnsUnreadMessages_ForUser()
        {
            // Arrange: Set up test data
            var userId = Guid.Parse(_httpContextAccessorMock.Object.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var workshopId = Guid.NewGuid();

            var workshop = new Shared.Models.WorkshopPanel.Workshop
            {
                Id = workshopId,
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

            var message1 = new Shared.Models.ExtraModels.Message
            {
                Id = Guid.NewGuid(),
                Content = "First test message",
                ReceiverUserId = userId,
                SenderWorkshopId = workshopId,
                SenderWorkshop = workshop,
                Car = car,
                IsRead = false,
                SentAt = DateTime.Now.AddDays(-1),
                DateOfVisit = DateTime.Now.AddDays(1),
                Service = ServiceType.MechanicalService,
                Responsed = false
            };

            var message2 = new Shared.Models.ExtraModels.Message
            {
                Id = Guid.NewGuid(),
                Content = "Second test message",
                ReceiverUserId = userId,
                SenderWorkshopId = workshopId,
                SenderWorkshop = workshop,
                Car = car,
                IsRead = false,
                SentAt = DateTime.Now.AddDays(-2),
                DateOfVisit = DateTime.Now.AddDays(2),
                Service = ServiceType.MechanicalService,
                Responsed = false
            };

            await _dbContext.Workshops.AddAsync(workshop);
            await _dbContext.Cars.AddAsync(car);
            await _dbContext.Messages.AddRangeAsync(message1, message2);
            await _dbContext.SaveChangesAsync();

            var query = new ReceiveMessageQuery();

            // Act: Execute the query handler
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert: Check the returned message details
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            var receivedMessage1 = result.FirstOrDefault(m => m.Id == message1.Id);
            Assert.NotNull(receivedMessage1);
            Assert.Equal("First test message", receivedMessage1.Message);
            Assert.Equal("Test Workshop", receivedMessage1.WorkshopName);
            Assert.False(receivedMessage1.IsRead);
            Assert.Equal(message1.SentAt, receivedMessage1.SentAt);
            Assert.Equal("Toyota", receivedMessage1.Brand);
            Assert.Equal("Camry", receivedMessage1.Model);
            Assert.Equal("MechanicalService", receivedMessage1.Service);

            var receivedMessage2 = result.FirstOrDefault(m => m.Id == message2.Id);
            Assert.NotNull(receivedMessage2);
            Assert.Equal("Second test message", receivedMessage2.Message);
            Assert.Equal("Test Workshop", receivedMessage2.WorkshopName);
            Assert.False(receivedMessage2.IsRead);
            Assert.Equal(message2.SentAt, receivedMessage2.SentAt);
            Assert.Equal("Toyota", receivedMessage2.Brand);
            Assert.Equal("Camry", receivedMessage2.Model);
            Assert.Equal("MechanicalService", receivedMessage2.Service);
        }
    }
}
