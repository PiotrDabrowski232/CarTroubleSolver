using CarTroubleSolver.Workshop.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Workshop.Data
{
    public class CarTroubleSolverDbContext(DbContextOptions<CarTroubleSolverDbContext> options) : DbContext(options)
    {
        public DbSet<Models.Workshop> Workshops { get; set; }
        public DbSet<Street> Streets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(CarTroubleSolverDbContext).Assembly);
        }
    }
}
