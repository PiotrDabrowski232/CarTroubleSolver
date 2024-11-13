using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.Enum;
using CarTroubleSolver.Shared.Repositories.Interfaces;
using CarTroubleSolver.Workshop.Logic.Functions.Accident;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace CarTroubleSolver.Workshop.Tests.Accident
{
    public class GetAccidentsQueryTest
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<IStatusHistoryRepository> _statusHistoryRepositoryMock;
        private readonly GetAccidentsQueryHandler _handler;

        public GetAccidentsQueryTest()
        {
            _dbContext = DbContextFactory.Create();
            DbContextFactory.AddTestData(_dbContext);

            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, _dbContext.Workshops.First().Id.ToString())
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            _httpContextAccessorMock.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            _statusHistoryRepositoryMock = new Mock<IStatusHistoryRepository>();

            _handler = new GetAccidentsQueryHandler(
                _httpContextAccessorMock.Object,
                _dbContext,
                _statusHistoryRepositoryMock.Object
            );
        }

        [Fact]
        public async Task Handle_Should_Return_Accidents_For_Workshop()
        {
            // Arrange
            var workshopId = _dbContext.Workshops.First().Id.ToString();

            // Act
            var query = new GetAccidentsQuery();
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            var expectedAccidents = _dbContext.Accidents
                .Where(x => x.WorkshopId == Guid.Parse(workshopId))
                .ToList();
            Assert.Equal(expectedAccidents.Count, result.Count);

            Assert.All(result, accident =>
            {
                var correspondingAccident = expectedAccidents.FirstOrDefault(a => a.Id == accident.Id);
                Assert.NotNull(correspondingAccident);
                Assert.Equal(Enum.GetName(correspondingAccident.Service), accident.Service);
                Assert.Equal(correspondingAccident.Car.VIN, accident.Vin);
            });
        }

        [Fact]
        public async Task Handle_Should_Return_Empty_List_When_No_Accidents_For_Workshop()
        {
            // Arrange
            var noAccidentWorkshop = new Shared.Models.WorkshopPanel.Workshop
            {
                Id = Guid.NewGuid(),
                Name = "Workshop without Accidents",
                Email = "test@workshop.com",
                NIP = 12345678901,
                PhoneNumber = 123123123,
                Location = new NetTopologySuite.Geometries.Point(0, 0),
                Password = "password",
            };
            _dbContext.Workshops.Add(noAccidentWorkshop);
            await _dbContext.SaveChangesAsync();

            _httpContextAccessorMock.Setup(x => x.HttpContext.User)
                .Returns(new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, noAccidentWorkshop.Id.ToString())
                }, "TestAuthType")));

            // Act
            var query = new GetAccidentsQuery();
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task Handle_Should_Return_Accidents_Ordered_By_Status_Then_Date()
        {
            // Arrange
            var workshopId = _dbContext.Workshops.First().Id;
            var accidents = _dbContext.Accidents
                .Where(x => x.WorkshopId == workshopId)
                .ToList();

            foreach (var accident in accidents)
            {
                var statusHistory = new Shared.Models.ExtraModels.StatusHistory
                {
                    ControlQueue = 1,
                    Status = AccidentStatus.Progress,
                    AccidentId = accident.Id
                };
                _dbContext.StatusHistory.Add(statusHistory);
            }

            await _dbContext.SaveChangesAsync();

            // Act
            var query = new GetAccidentsQuery();
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.SequenceEqual(result.OrderBy(x => x.Status).ThenByDescending(x => x.Date)));
        }
    }
}
