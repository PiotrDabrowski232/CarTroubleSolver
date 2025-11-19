using CarTroubleSolver.Workshop.Logic.Functions.Services;

namespace CarTroubleSolver.Workshop.Tests.Services
{
    public class GetServiceTypesQueryTest
    {
        private readonly GetServiceTypesQueryHandler _handler;

        public GetServiceTypesQueryTest()
        {
            _handler = new GetServiceTypesQueryHandler();
        }

        [Fact]
        public void GetServiceTypesQuery_Returns_AllServices()
        {
            //Act
            var command = new GetServiceTypesQuery();
            var result = _handler.Handle(command, default);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.Result.Count());
        }
    }
}
