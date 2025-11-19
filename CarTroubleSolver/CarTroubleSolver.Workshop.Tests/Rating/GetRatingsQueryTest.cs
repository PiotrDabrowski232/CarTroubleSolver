using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Workshop.Logic.Functions.Rating;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace CarTroubleSolver.Workshop.Tests.Rating
{
    public class GetRatingsQueryTest
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly GetRatingsQueryHandler _handler;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;

        public GetRatingsQueryTest()
        {

            _dbContext = DbContextFactory.Create();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _handler = new GetRatingsQueryHandler(_dbContext, _mockHttpContextAccessor.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_RatingDto_With_Average_And_Items()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var workshopId = Guid.NewGuid();

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, workshopId.ToString()) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            _mockHttpContextAccessor.Setup(con => con.HttpContext.User).Returns(claimsPrincipal);

            var workshop = new Shared.Models.WorkshopPanel.Workshop
            {
                Id = workshopId,
                Name = "Test Workshop",
                Ratings = new List<Shared.Models.WorkshopPanel.Rating>
                {
                    new Shared.Models.WorkshopPanel.Rating
                    {
                        Rate = 4,
                        Comment = "Great service!",
                        Date = DateTime.UtcNow,
                        User = new Shared.Models.UserPanel.User { Name = "John", Surname = "Doe", Email= "user@o2.pl", Password="Password123!", DateOfBirth = new DateOnly(2000,12,12) }
                    },
                    new Shared.Models.WorkshopPanel.Rating
                    {
                        Rate = 3,
                        Comment = "Okay service.",
                        Date = DateTime.UtcNow,
                        User = new Shared.Models.UserPanel.User { Name = "Jane", Surname = "Smith", Email= "user2@o2.pl", Password="Password123!", DateOfBirth = new DateOnly(2000,12,12) }
                    }
                },
                Email = "workshop@o2.pl",
                Location = new NetTopologySuite.Geometries.Point(0, 0),
                Password = "Password123!"
            };

            _dbContext.Workshops.Add(workshop);
            await _dbContext.SaveChangesAsync();

            var query = new GetRatingsQuery();
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Workshop", result.WorkshopName);
            Assert.Equal(3.5, result.Average);

            Assert.Equal(2, result.RatingItems.Count);
            Assert.Contains(result.RatingItems, item => item.Content == "Great service!" && item.Rate == 4 && item.UserName == "John Doe");
            Assert.Contains(result.RatingItems, item => item.Content == "Okay service." && item.Rate == 3 && item.UserName == "Jane Smith");
        }

        [Fact]
        public async Task Handle_Should_Return_Null_If_No_Workshop_Found()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();

            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            _mockHttpContextAccessor.Setup(con => con.HttpContext.User).Returns(claimsPrincipal);

            // Act
            var query = new GetRatingsQuery();
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
