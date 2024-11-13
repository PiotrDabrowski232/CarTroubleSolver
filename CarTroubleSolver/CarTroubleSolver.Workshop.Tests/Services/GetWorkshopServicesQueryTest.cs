using AutoMapper;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Workshop.Logic.Functions.Services;
using Moq;

namespace CarTroubleSolver.Workshop.Tests.Services
{
    public class GetWorkshopServicesQueryTest
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly Mock<IMapper> _mockMapper;
        private readonly GetWorkshopServicesQueryHandler _handler;

        public GetWorkshopServicesQueryTest()
        {
            _dbContext = DbContextFactory.Create();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetWorkshopServicesQueryHandler(_dbContext, _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_Should_Return_Workshop_Services()
        {
            // Arrange
            var workshopId = Guid.NewGuid();
            var workshop = new Shared.Models.WorkshopPanel.Workshop
            {
                Id = workshopId,
                Email = "workshop@o2.pl",
                Name = "workshop",
                Password = "Password123!",
                PhoneNumber = 123123333,
                NIP = 12312312312,
                Location = new NetTopologySuite.Geometries.Point(0, 0)
            };

            var service1 = new Shared.Models.WorkshopPanel.WorkshopServices
            {
                Id = Guid.NewGuid(),
                Service = Shared.Models.Enum.ServiceType.MechanicalService,
                Price = 150,
                WorkshopId = workshopId
            };

            var service2 = new Shared.Models.WorkshopPanel.WorkshopServices
            {
                Id = Guid.NewGuid(),
                Service = Shared.Models.Enum.ServiceType.CarInspection,
                Price = 200,
                WorkshopId = workshopId
            };

            _dbContext.Workshops.Add(workshop);
            _dbContext.WorkshopServices.AddRange(service1, service2);
            await _dbContext.SaveChangesAsync();

            // Act
            var query = new GetWorkshopServicesQuery(workshopId);
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, x => x.ServiceType == Enum.GetName(Shared.Models.Enum.ServiceType.MechanicalService));
            Assert.Contains(result, x => x.ServiceType == Enum.GetName(Shared.Models.Enum.ServiceType.CarInspection));
            Assert.Contains(result, x => x.Price == 150);
            Assert.Contains(result, x => x.Price == 200);
        }
    }
}
