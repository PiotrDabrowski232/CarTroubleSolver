using CarTroubleSolver.Workshop.Logic.Functions.Street.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Workshop.Tests.Street
{
    public class GetAllPrivincesQueryTest
    {
        private readonly GetAllProvincesQueryHandler _handler;

        public GetAllPrivincesQueryTest()
        {
            _handler = new GetAllProvincesQueryHandler();
        }

        [Fact]
        public void GetAllProvincesQueryHandler_Returned_AllProvinces()
        {
            //Act
            var command = new GetAllProvincesQuery();
            var result = _handler.Handle(command, default);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(16, result.Result.Province.Count);
        }
    }
}
