using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.Enums;
using CarTroubleSolver.Shared.Models.Enums.Models;
using CarTroubleSolver.Shared.Models.ExtraModels;
using CarTroubleSolver.Shared.Models.UserPanel;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Workshop.Tests
{
    public class DbContextFactory
    {
        public static CarTroubleSolverDbContext Create()
        {
            var options = new DbContextOptionsBuilder<CarTroubleSolverDbContext>()
                .UseInMemoryDatabase(databaseName: "CarTroubleSolverTestDb")
                .Options;

            var dbContext = new CarTroubleSolverDbContext(options);
            dbContext.Database.EnsureCreated();
            return dbContext;
        }

        public static void AddTestData(CarTroubleSolverDbContext dbContext)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = "user@o2.pl",
                Name = "User",
                Password = "Password123!",
                PhoneNumber = 123123123,
                Surname = "test",
                DateOfBirth = new DateOnly(2000, 12, 12)
            };
            dbContext.Users.Add(user);

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

            dbContext.Workshops.Add(workshop);


            var service = new WorkshopServices
            {
                Id  =  Guid.NewGuid(),
                Service = Shared.Models.Enum.ServiceType.MechanicalService,
                Price = 120,
                WorkshopId = workshop.Id,
            };

            dbContext.WorkshopServices.Add(service);

            var car = new Car
            {
                Id = Guid.NewGuid(),
                VIN = "asd123adq13513acdfaw",
                Brand = Brand.Toyota,
                Model = ToyotaModels.Corolla.ToString(),
                Mileage = 123333,
                Engine = "v8",
                DoorCount = 5,
                DateOfProduction = new DateTime(2000, 12, 12),
                CarType = CarType.Sedan,
                OwnerId = user.Id,
                Color = new CarColor { Red = 120, Green = 120, Blue = 120 }
            };

            dbContext.Cars.Add(car);

            var userSendMessage = new Message
            {
                Id = Guid.NewGuid(),
                Content = "content",
                SentAt = DateTime.Now,
                SenderUserId = user.Id,
                ReceiverWorkshopId = workshop.Id,
                CarId = car.Id,
                IsRead = true,
                Responsed = false,
                RateMessage = false,
                Service = service.Service,

            };

            dbContext.Messages.Add(userSendMessage);

            var WorkshopSendMessage = new Message
            {
                Id = Guid.NewGuid(),
                Content = "content",
                SentAt = DateTime.Now,
                ReceiverUserId = user.Id,
                SenderWorkshopId = workshop.Id,
                CarId = car.Id,
                IsRead = true,
                Responsed = false,
                RateMessage = false,
                Service = service.Service,
                PreviousMessageId = userSendMessage.Id
            };

            dbContext.Messages.Add(WorkshopSendMessage);


            var accident = new Shared.Models.ExtraModels.Accident
            {
                Id = Guid.NewGuid(),
                WorkshopId = workshop.Id,
                CarId = car.Id,
                ProblemDescription = "Problem",
                Service = service.Service,
            };

            dbContext.Accidents.Add(accident);

            var repairHistory = new RepairHistory
            {
                Id = Guid.NewGuid(),
                Service = service.Service,
                Price = (int)service.Price,
                CarId = car.Id,
                WorkshopId = workshop.Id,
                AccidentId = accident.Id,
            };

            dbContext.RepairHistory.Add(repairHistory);

            HistoryItems histryItem = new HistoryItems
            {
                Id = Guid.NewGuid(),
                Name = "Name",
                Price = 123.55f,
                Amount = 2,
                RepairHistoryId = repairHistory.Id,
            };

            dbContext.HistoryItems.Add(histryItem);

            var configurations = new List<HourConfiguration>
            {
                new HourConfiguration
                {
                    DayOfWeek = DayOfWeek.Monday,
                    From = new TimeOnly(8, 0), 
                    To = new TimeOnly(16, 0),   
                    WorkshopId = workshop.Id
                },
                
                new HourConfiguration
                {
                    DayOfWeek = DayOfWeek.Tuesday,
                    From = new TimeOnly(9, 0), 
                    To = new TimeOnly(17, 0),   
                    WorkshopId = workshop.Id
                },
                
                new HourConfiguration
                {
                    DayOfWeek = DayOfWeek.Wednesday,
                    From = new TimeOnly(10, 0), 
                    To = new TimeOnly(18, 0),   
                    WorkshopId = workshop.Id
                },
                
                new HourConfiguration
                {
                    DayOfWeek = DayOfWeek.Thursday,
                    From = new TimeOnly(9, 0),  
                    To = new TimeOnly(17, 0),   
                    WorkshopId = workshop.Id
                },
                
                new HourConfiguration
                {
                    DayOfWeek = DayOfWeek.Friday,
                    From = new TimeOnly(8, 0),  
                    To = new TimeOnly(14, 0),  
                    WorkshopId = workshop.Id
                }
            };

            dbContext.Hours.AddRange(configurations);
            dbContext.SaveChanges();
        }
    }
}
