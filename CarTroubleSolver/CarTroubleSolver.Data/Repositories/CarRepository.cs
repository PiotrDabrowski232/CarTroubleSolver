using CarTroubleSolver.Data.Data;
using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Data.Repositories.Interfaces;

namespace CarTroubleSolver.Data.Repositories
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

    }
}
