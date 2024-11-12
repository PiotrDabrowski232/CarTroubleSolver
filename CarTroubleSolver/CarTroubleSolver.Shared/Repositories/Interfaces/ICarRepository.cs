using CarTroubleSolver.Shared.Models.UserPanel;
using NetTopologySuite.Geometries;

namespace CarTroubleSolver.Shared.Repositories.Interfaces
{
    public interface ICarRepository : IGenericRepository<Car>
    {
        public Task<bool> DeleteCarByVinNumber(string vin);
        public Task UpdateImagePath(string Vin, string Path);
        public string? UpdateCarByVinAsync(Car car, string vin);
    }
}
