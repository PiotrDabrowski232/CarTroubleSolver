using CarTroubleSolver.Shared.Models.ExtraModels;
using CarTroubleSolver.Shared.Models.UserPanel;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using Microsoft.EntityFrameworkCore;

namespace CarTroubleSolver.Shared.Data
{
    public class CarTroubleSolverDbContext(DbContextOptions<CarTroubleSolverDbContext> options) : DbContext(options)
    {
        public DbSet<Workshop> Workshops { get; set; }
        public DbSet<WorkshopServices> WorkshopServices { get; set; }
        public DbSet<HourConfiguration> Hours { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Role> Roles { get; set; } 
        public DbSet<Rating> Rating { get; set; } 
        public DbSet<Message> Messages { get; set; } 
        public DbSet<Accident> Accidents { get; set; } 
        public DbSet<StatusHistory> StatusHistory { get; set; } 
        public DbSet<RepairHistory> RepairHistory { get; set; } 
        public DbSet<HistoryItems> HistoryItems { get; set; } 

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(CarTroubleSolverDbContext).Assembly);
        }
    }
}
