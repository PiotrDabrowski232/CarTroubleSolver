using Azure.Core;
using CarTroubleSolver.Data.Models;

namespace CarTroubleSolver.Data.Repositories.Interfaces
{
    public interface ICarRepository : IGenericRepository<Car>
    {
        public Task DeleteCarByVinNumber(long vin);
        public Task UpdateImagePath(long Vin, string Path);
        public string? UpdateCarByVinAsync(Car car, long vin);
    }
}
