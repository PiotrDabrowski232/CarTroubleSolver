using CarTroubleSolver.Shared.Models.UserPanel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTroubleSolver.Data.ModelsConfiguration
{
    internal class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(u => u.Id);

            builder
            .Property(u => u.DateOfProduction)
            .HasColumnType("date");
        }
    }
}
