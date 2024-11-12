using CarTroubleSolver.Shared.Data;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Tests
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
    }
}
