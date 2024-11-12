using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.UserPanel;
using CarTroubleSolver.Shared.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Shared.Repositories
{
    public class CarRepository(CarTroubleSolverDbContext dbContext) : GenericRepository<Car>(dbContext), IGenericRepository<Car>, ICarRepository
    {
        public async Task<bool> DeleteCarByVinNumber(string vin)
        {
            var car = await dbContext.Cars.Where(x => x.VIN == vin)
                .Include(x => x.Accidents)
                .Include(x => x.RepairHistory)
                .FirstOrDefaultAsync(default);

            dbContext.RepairHistory.RemoveRange(car.RepairHistory);
            dbContext.Accidents.RemoveRange(car.Accidents);

            dbContext.Cars.Remove(car);
            dbContext.SaveChanges();

            return true;
        }

        public Task UpdateImagePath(string Vin, string Path)
        {
            var car = dbContext.Cars.First(x => x.VIN == Vin);
            car.ImagePath = Path;
            dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public string? UpdateCarByVinAsync(Car car, string vin)
        {
            var existingCar = dbContext.Cars.FirstOrDefault(x => x.VIN == vin.ToUpper());

            if (existingCar != null)
            {
                existingCar.VIN = car.VIN.ToUpper();
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
