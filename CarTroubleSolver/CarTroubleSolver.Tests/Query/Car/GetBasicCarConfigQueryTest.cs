using CarTroubleSolver.Logic.Functions.Car.Query;

namespace CarTroubleSolver.Tests.Query.Car
{
    public class GetBasicCarConfigQueryHandlerTests
    {
        private readonly GetBasicCarConfigQueryHandler _handler;

        public GetBasicCarConfigQueryHandlerTests()
        {
            _handler = new GetBasicCarConfigQueryHandler();
        }

        [Fact]
        public async Task Handle_ShouldReturnBasicCarConfigWithBrandsAndTypes()
        {
            // Arrange
            var query = new GetBasicCarConfigQuery();

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result.Brands);
            Assert.Contains("BMW", result.Brands);
            Assert.Contains("Audi", result.Brands);

            Assert.NotEmpty(result.Types);
            Assert.Contains("Sedan", result.Types);
            Assert.Contains("Hatchback", result.Types);
        }
    }
}