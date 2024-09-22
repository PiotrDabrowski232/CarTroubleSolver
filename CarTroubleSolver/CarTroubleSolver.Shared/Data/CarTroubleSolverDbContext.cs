using CarTroubleSolver.Shared.Models.UserPanel;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Shared.Data
{
    public class CarTroubleSolverDbContext(DbContextOptions<CarTroubleSolverDbContext> options) : DbContext(options)
    {
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<HourConfiguration> Hours { get; set; }
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
