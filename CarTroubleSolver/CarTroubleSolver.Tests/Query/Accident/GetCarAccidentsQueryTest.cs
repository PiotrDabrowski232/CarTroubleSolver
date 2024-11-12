using CarTroubleSolver.Logic.Functions.Accident;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.Enums;
using CarTroubleSolver.Shared.Models.UserPanel;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Tests.Query.Accident
{
    public class GetCarAccidentsQueryTest
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly GetCarAccidentsQueryHandler _handler;

        public GetCarAccidentsQueryTest()
        {
            _dbContext = DbContextFactory.Create();
            _handler = new GetCarAccidentsQueryHandler(_dbContext);
        }

        [Fact]
        public async Task GetCarAccidentsQuery_Returns_AccidentsRewrittenToCar()
        {
            //Arrange
            var car = new Shared.Models.UserPanel.Car
            {
                Id = Guid.NewGuid(),
                VIN = "1HGBH41JXMN109186",
                Brand = Brand.Toyota,
                Model = "Corolla",
                Color = new CarColor { Red = 120, Green = 120, Blue = 120 },
                DoorCount = 4,
                DateOfProduction = new DateTime(2020, 5, 10),
                CarType = CarType.Sedan,
                Mileage = 15000,
                Engine = "1.8L 4-cylinder",
            };

            var workshop = new Shared.Models.WorkshopPanel.Workshop
            {
                Id = Guid.NewGuid(),
                Name = "WorkshopName",
                Email = "worskhop@o2.pl",
                Location = new NetTopologySuite.Geometries.Point(53.23, 55.22),
                Password = "Password123!"
            };

            var accident = new Shared.Models.ExtraModels.Accident()
            {
                Id = Guid.NewGuid(),
                WorkshopId = Guid.NewGuid(),
                CarId = car.Id,
                ProblemDescription = "problem",
                Service = Shared.Models.Enum.ServiceType.MechanicalService,
                StartDate = DateTime.Now, 
                Workshop = workshop,
            };

            var accidents = new List<Shared.Models.ExtraModels.Accident>() { accident};
            car.Accidents = accidents;


            _dbContext.Cars.Add(car);
            await _dbContext.SaveChangesAsync();


            //Act
            var query = new GetCarAccidentsQuery(car.VIN);
            var result = await _handler.Handle(query, default);


            //Assert
            Assert.True(await _dbContext.Cars.AnyAsync(c => c.VIN == car.VIN));
            Assert.True(await _dbContext.Accidents.AnyAsync(a => a.CarId == car.Id));

            Assert.NotNull(result);
            Assert.Equal(accidents.Count, result.Count);
            Assert.Equal(accidents.First().Id, result.First().Id);

        }
    }
}
