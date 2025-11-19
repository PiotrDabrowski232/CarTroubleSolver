using AutoMapper;
using CarTroubleSolver.Shared.Exceptions;
using CarTroubleSolver.Shared.Models.ExtraModels;
using CarTroubleSolver.Shared.Repositories.Interfaces;
using CarTroubleSolver.Shared.Services.Interface;
using CarTroubleSolver.Workshop.Logic.Dto.Hour;
using CarTroubleSolver.Workshop.Logic.Dto.Workshop;
using CarTroubleSolver.Workshop.Logic.Functions.Workshop.Query;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CarTroubleSolver.Workshop.Tests.Workshop
{
    public class LoginQueryTest
    {
        private readonly Mock<IWorkshopRepository> _mockWorkshopRepository;
        private readonly Mock<IHashingService> _mockHashingService;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly Mock<IGeoLocalizationService> _mockGeoLocalizationService;
        private readonly Mock<IMapper> _mockMapper;
        private readonly LoginQueryHandler _handler;

        public LoginQueryTest()
        {
            _mockWorkshopRepository = new Mock<IWorkshopRepository>();
            _mockHashingService = new Mock<IHashingService>();
            _mockTokenService = new Mock<ITokenService>();
            _mockGeoLocalizationService = new Mock<IGeoLocalizationService>();
            _mockMapper = new Mock<IMapper>();

            _handler = new LoginQueryHandler(
                _mockWorkshopRepository.Object,
                _mockHashingService.Object,
                _mockTokenService.Object,
                _mockGeoLocalizationService.Object,
                _mockMapper.Object
            );
        }

        [Fact]
        public async Task Handle_Returns_LoginResponse_When_CredentialsAreValid()
        {
            // Arrange
            var loginWorkshopDto = new LoginWorkshopDto
            {
                Email = "workshop@o2.pl",
                Password = "Password123!"
            };

            var loginQuery = new LoginQuery(loginWorkshopDto);

            var workshop = new CarTroubleSolver.Shared.Models.WorkshopPanel.Workshop
            {
                Id = Guid.NewGuid(),
                Email = "workshop@o2.pl",
                Password = "HashedPassword123!", 
                Location = new NetTopologySuite.Geometries.Point(1.0, 1.0),
                OpenHours = new List<CarTroubleSolver.Shared.Models.WorkshopPanel.HourConfiguration>()
            };

            var hashedPassword = "HashedPassword123!"; 

            var workshopDetailsDto = new WorkshopDetailsDto
            {
                Id = workshop.Id.ToString(),
                Name = workshop.Name,
                Email = workshop.Email,
                PhoneNumber = workshop.PhoneNumber,
                NIP = workshop.NIP,
                Hours = new List<HoursDto>()
            };
            var street = new StreetDto
            {
                StreetName = "street",
                StreetNumber = "12/5",
                PostalCode= "123",
                City = "Bia",
                Country = "pol",
                Province = "podl"
            };

            _mockWorkshopRepository
                .Setup(repo => repo.GetAll())
                .Returns(new List<CarTroubleSolver.Shared.Models.WorkshopPanel.Workshop> { workshop }.AsQueryable());

            _mockHashingService
                .Setup(service => service.VerifyHashedPassword(It.IsAny<Account>(), workshop.Password, loginWorkshopDto.Password))
                .Returns(true); 

            _mockTokenService
                .Setup(service => service.GenerateJwt(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns("dummy-jwt-token");

            _mockGeoLocalizationService
                .Setup(service => service.GetLocalizationDetails(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(street);

            _mockMapper
                .Setup(mapper => mapper.Map<ICollection<HoursDto>?>(It.IsAny<IEnumerable<CarTroubleSolver.Shared.Models.WorkshopPanel.HourConfiguration>>()))
                .Returns(new List<HoursDto>());

            // Act
            var result = await _handler.Handle(loginQuery, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("dummy-jwt-token", result.JWT);
            Assert.Equal(workshopDetailsDto.Name, result.WorkshopDetails.Name);
            Assert.Equal(workshopDetailsDto.Email, result.WorkshopDetails.Email);
            Assert.Equal(street.StreetName, result.WorkshopDetails.Adress.StreetName);
            _mockHashingService.Verify(x => x.VerifyHashedPassword(It.IsAny<Account>(), workshop.Password, loginWorkshopDto.Password), Times.AtLeastOnce);
        }

        [Fact]
        public async Task Handle_Throws_InvalidProvidedDataException_When_WorkshopNotFound()
        {
            // Arrange
            var loginWorkshopDto = new LoginWorkshopDto
            {
                Email = "nonexistent@o2.pl",
                Password = "Password123!"
            };

            var loginQuery = new LoginQuery(loginWorkshopDto);

            _mockWorkshopRepository
                .Setup(repo => repo.GetAll())
                .Returns(Enumerable.Empty<CarTroubleSolver.Shared.Models.WorkshopPanel.Workshop>().AsQueryable());

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidProvidedDataException>(() => _handler.Handle(loginQuery, CancellationToken.None));
            Assert.Equal("There is not workshop with provided email", exception.Message);
        }

        [Fact]
        public async Task Handle_Throws_InvalidProvidedDataException_When_PasswordIsIncorrect()
        {
            // Arrange
            var loginWorkshopDto = new LoginWorkshopDto
            {
                Email = "workshop@o2.pl",
                Password = "WrongPassword"
            };

            var loginQuery = new LoginQuery(loginWorkshopDto);

            var workshop = new CarTroubleSolver.Shared.Models.WorkshopPanel.Workshop
            {
                Id = Guid.NewGuid(),
                Email = "workshop@o2.pl",
                Password = "HashedPassword123!", 
                Location = new NetTopologySuite.Geometries.Point(1.0, 1.0),
                OpenHours = new List<CarTroubleSolver.Shared.Models.WorkshopPanel.HourConfiguration>()
            };

            _mockWorkshopRepository
                .Setup(repo => repo.GetAll())
                .Returns(new List<CarTroubleSolver.Shared.Models.WorkshopPanel.Workshop> { workshop }.AsQueryable());

            _mockHashingService
                .Setup(service => service.VerifyHashedPassword(It.IsAny<Account>(), workshop.Password, loginWorkshopDto.Password))
                .Returns(false); 

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidProvidedDataException>(() => _handler.Handle(loginQuery, CancellationToken.None));
            Assert.Equal("Incorrect Password", exception.Message);
        }
    }
}
