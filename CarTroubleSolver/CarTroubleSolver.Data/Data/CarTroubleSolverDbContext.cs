using CarTroubleSolver.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Data.Data
{
    public class CarTroubleSolverDbContext(DbContextOptions<CarTroubleSolverDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(CarTroubleSolverDbContext).Assembly);
        }
    }
}
