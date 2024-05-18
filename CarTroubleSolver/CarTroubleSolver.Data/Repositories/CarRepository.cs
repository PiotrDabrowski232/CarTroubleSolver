using CarTroubleSolver.Data.Data;
using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Data.Repositories
{
    public class CarRepository(CarTroubleSolverDbContext dbContext) : GenericRepository<Car>(dbContext), IGenericRepository<Car>, ICarRepository
    {
        public Task<Car> Add(Car entity)
        {
            _context.Cars.Add(entity);
            _context.SaveChanges();
            return Task.FromResult(entity);
        }
    }
}
