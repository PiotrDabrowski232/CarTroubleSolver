using CarTroubleSolver.Data.Models;

namespace CarTroubleSolver.Data.Repositories.Interfaces
{
    public interface ICarRepository : IGenericRepository<Car>
    {
        public Task DeleteCarByVinNumber(long vin);
    }
}
