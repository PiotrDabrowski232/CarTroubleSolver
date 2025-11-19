using AutoMapper;
using CarTroubleSolver.Shared.Models.ExtraModels;
using CarTroubleSolver.Shared.Repositories.Interfaces;
using CarTroubleSolver.Shared.Services.Interface;
using CarTroubleSolver.Workshop.Logic.Dto.Workshop;
using CarTroubleSolver.Workshop.Logic.Functions.Workshop.Command;
using Moq;

namespace CarTroubleSolver.Workshop.Tests.Workshop
{
    public class RegisterWorkshopCommandTest
    {
        private readonly Mock<IWorkshopRepository> _mockRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IGeoLocalizationService> _mockGeoService;
        private readonly Mock<IHashingService> _mockHashService;
        private readonly RegiserWorkshopCommandHandler _handler;

        public RegisterWorkshopCommandTest()
        {
            _mockGeoService = new Mock<IGeoLocalizationService>();
            _mockHashService = new Mock<IHashingService>();
            _mockRepository = new Mock<IWorkshopRepository>();
            _mockMapper = new Mock<IMapper>();
            _handler = new RegiserWorkshopCommandHandler(_mockRepository.Object, _mockMapper.Object, _mockGeoService.Object, _mockHashService.Object);
        }

        [Fact]
        public async Task RegiserWorkshopCommand_Returns_Success()
        {
            //arrange
            var workshop = new Shared.Models.WorkshopPanel.Workshop
            {
                Id = Guid.NewGuid(),
                Email = "workshop@o2.pl",
                Name = "workshop",
                Password = "Password123!",
                PhoneNumber = 123123333,
                NIP = 12312312312,
                Location = new NetTopologySuite.Geometries.Point(0, 0)
            };

            _mockMapper.Setup(x => x.Map<Shared.Models.WorkshopPanel.Workshop>(It.IsAny<RegisterWorkshopDto>())).Returns(workshop);
            _mockGeoService.Setup(x => x.GetCurrentGeoLocalization(It.IsAny<StreetDto>(), default)).ReturnsAsync(((decimal)workshop.Location.X, (decimal)workshop.Location.Y));
            _mockRepository.Setup(x => x.Add(It.IsAny<Shared.Models.WorkshopPanel.Workshop>())).Returns(workshop.Id);
            _mockHashService.Setup(x => x.HashPassword(null, It.IsAny<string>())).Returns("hashedPassword");


            //act
            var command = new RegiserWorkshopCommand(new RegisterWorkshopDto
            {
                Email = workshop.Email,
                Name = workshop.Name,
                Password = workshop.Password,
                PhoneNumber = workshop.PhoneNumber,
                NIP = workshop.NIP,
                Street = new StreetDto { StreetName = "Baker Street", PostalCode = "221B" }
            });

            var result = await _handler.Handle(command, CancellationToken.None);


            //assert
            Assert.IsType<Guid>(result); 
            Assert.NotEqual(Guid.Empty, result);
        }

    }
}
