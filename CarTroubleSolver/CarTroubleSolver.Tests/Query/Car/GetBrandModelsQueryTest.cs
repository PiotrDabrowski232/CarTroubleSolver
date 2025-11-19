using CarTroubleSolver.Logic.Factories.Car.Model.ModelFactory;
using CarTroubleSolver.Logic.Functions.Car.Query;
using CarTroubleSolver.Shared.Models.Enums.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CarTroubleSolver.Tests.Query.Car
{
    public class GetBrandModelsQueryTest
    {
        private readonly GetBrandModelsQueryHandler _handler;
        public GetBrandModelsQueryTest()
        {
            ModelFactory modelFactory = new ModelFactory();
            _handler = new GetBrandModelsQueryHandler(modelFactory);
        }

        [Fact]
        public async Task GetBrandModelsQueryHandler_Returned_ModelForCurrentBrand()
        {
            //Arrange
            var query = new GetBrandModelsQuery("BMW");

            //Act
            var result = await _handler.Handle(query, default);


            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(24, result.Count);
        }
    }
}
