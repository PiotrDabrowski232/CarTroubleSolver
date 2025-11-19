using AutoMapper;
using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using CarTroubleSolver.Workshop.Logic.Dto.Service;
using CarTroubleSolver.Workshop.Logic.Functions.Services;
using Moq;

namespace CarTroubleSolver.Workshop.Tests.Services
{
    public class AddServiceForWorkshopCommandTest
    {
        private readonly CarTroubleSolverDbContext _dbcontext;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AddServicesForWorkshopCommandHandler _handler;

        public AddServiceForWorkshopCommandTest()
        {
            _dbcontext = DbContextFactory.Create();

            _mockMapper = new Mock<IMapper>();

            _handler = new AddServicesForWorkshopCommandHandler(_dbcontext,  _mockMapper.Object);
        }

        [Fact]
        public async Task Handle_Should_Remove_Existing_Services_And_Add_New_One()
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

            var existingService = new WorkshopServices
            {
                Id = Guid.NewGuid(),
                Service = Shared.Models.Enum.ServiceType.MechanicalService,
                Price = 120,
                WorkshopId = workshopId,
            };

            var newServiceDto = new ServiceDto
            {
                ServiceType = Shared.Models.Enum.ServiceType.MechanicalService.ToString(),
                Price = 200,
            };
            var newServiceDtos = new List<ServiceDto> { newServiceDto };

            _dbcontext.Workshops.Add(workshop);
            _dbcontext.WorkshopServices.Add(existingService);
            await _dbcontext.SaveChangesAsync();

            _mockMapper.Setup(x => x.Map<WorkshopServices>(It.IsAny<ServiceDto>()))
                      .Returns(new WorkshopServices
                      {
                          Service = Shared.Models.Enum.ServiceType.MechanicalService,
                          Price = 200
                      });

            // Act
            var command = new AddServicesForWorkshopCommand(workshopId, newServiceDtos);
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            var remainingServices = _dbcontext.WorkshopServices.ToList();

            Assert.Equal(newServiceDtos.Count, result.Count()); 
            Assert.Equal(newServiceDto.ServiceType, remainingServices[0].Service.ToString()); 

            var dbServices = _dbcontext.WorkshopServices.ToList();
            Assert.Contains(dbServices, s => s.Service == Shared.Models.Enum.ServiceType.MechanicalService && s.Price == 200);
        }
    }
}
