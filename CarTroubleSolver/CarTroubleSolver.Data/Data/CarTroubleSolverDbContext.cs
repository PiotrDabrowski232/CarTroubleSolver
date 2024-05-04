using CarTroubleSolver.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Data.Data
{
    public class CarTroubleSolverDbContext(DbContextOptions<CarTroubleSolverDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(CarTroubleSolverDbContext).Assembly);
        }
    }
}
