using CarTroubleSolver.Logic.Dto;
using CarTroubleSolver.Logic.Dto.Workshop;
using CarTroubleSolver.Logic.Functions.Workshop;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.Enum;
using CarTroubleSolver.Shared.Models.ExtraModels;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using CarTroubleSolver.Shared.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CarTroubleSolver.Tests.Query.Workshop
{
    public class WorkshopDetailsQueryTest
    {
        private readonly CarTroubleSolverDbContext _dbContext;
        private readonly WorkshopDetailsQueryHandler _handler;
        private readonly Mock<IGeoLocalizationService> _geoLocalizationServiceMock;

        public WorkshopDetailsQueryTest()
        {
            // Setting up in-memory database context for testing
            var options = new DbContextOptionsBuilder<CarTroubleSolverDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new CarTroubleSolverDbContext(options);

            // Mocking the geo localization service
            _geoLocalizationServiceMock = new Mock<IGeoLocalizationService>();
            _geoLocalizationServiceMock
                .Setup(service => service.GetLocalizationDetails(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new StreetDto
                {
                    City = "Test City",
                    StreetName = "Street",
                    Country = "Poland",
                    Province = "Podlaskie",
                    PostalCode = "12345"
                });

            _handler = new WorkshopDetailsQueryHandler(_dbContext, _geoLocalizationServiceMock.Object);
        }

        [Fact]
        public async Task WorkshopDetailsQuery_ReturnsCorrectWorkshopDetails_ForGivenWorkshopId()
        {
            // Arrange
            var workshopId = Guid.NewGuid();

            var workshop = new Shared.Models.WorkshopPanel.Workshop
            {
                Id = workshopId,
                Name = "Test Workshop",
                PhoneNumber = 123456789,
                Email = "workshop@o2.pl",
                Password = "Password123!",
                NIP = 1234567890,
                Location = new NetTopologySuite.Geometries.Point(10.0, 20.0) { SRID = 4326 },
                Services = new List<WorkshopServices>
                {
                    new WorkshopServices
                    {
                        Service = ServiceType.MechanicalService,
                        Price = 100
                    }
                },
                Ratings = new List<Rating>
                {
                    new Rating
                    {
                        Comment = "Great service!",
                        Rate = 5,
                        Date = DateTime.Now,
                        User = new Shared.Models.UserPanel.User { Name = "John", Surname = "Doe", Email= "user@o2.pl", Password = "Password123!", PhoneNumber=123123123,  DateOfBirth= new DateOnly(2000,12,12)}
                    }
                }
            };

            _dbContext.Workshops.Add(workshop);
            await _dbContext.SaveChangesAsync();

            var query = new WorkshopDetailsQuery(workshopId.ToString());

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Workshop", result.Name);
            Assert.Equal(123456789, result.PhoneNumber);
            Assert.Equal(1234567890, result.NIP);
            Assert.Single(result.Services);
            Assert.Equal("MechanicalService", result.Services[0].Service);
            Assert.Equal(100, result.Services[0].Price);
            Assert.Single(result.RateDetails);
            Assert.Equal("Great service!", result.RateDetails[0].Comment);
            Assert.Equal(5, result.RateDetails[0].Rate);
            Assert.Equal("John Doe", result.RateDetails[0].User);
            Assert.Equal("Test City", result.Location.City);
            Assert.Equal("Street", result.Location.StreetName);
            Assert.Equal("12345", result.Location.PostalCode);
        }
    }
}
