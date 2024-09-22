using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.UserPanel;
using CarTroubleSolver.Shared.Repositories.Interfaces;
using CarTroubleSolver.Shared.Repositories;
using CarTroubleSolver.Shared.Repositories.Interfaces;

namespace CarTroubleSolver.Shared.Repositories
{
    public class CarRepository(CarTroubleSolverDbContext dbContext) : GenericRepository<Car>(dbContext), IGenericRepository<Car>, ICarRepository
    {
        public Task DeleteCarByVinNumber(long vin)
        {
            var car = dbContext.Cars.First(x => x.VIN == vin);

            dbContext.Cars.Remove(car);
            dbContext.SaveChanges();

            return Task.CompletedTask;
        }

        public Task UpdateImagePath(long Vin, string Path)
        {
            var car = dbContext.Cars.First(x => x.VIN == Vin);
            car.ImagePath = Path;
            dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public string? UpdateCarByVinAsync(Car car, long vin)
        {
            var existingCar = dbContext.Cars.FirstOrDefault(x => x.VIN == vin);

            if (existingCar != null)
            {
                existingCar.VIN = car.VIN;
                existingCar.Brand = car.Brand;
                existingCar.Model = car.Model;
                existingCar.Mileage = car.Mileage;
                existingCar.Engine = car.Engine;
                existingCar.DoorCount = car.DoorCount;
                existingCar.DateOfProduction = car.DateOfProduction;
                existingCar.CarType = car.CarType;

                dbContext.SaveChanges();
            }

            return existingCar.ImagePath;
        }
    }
}
