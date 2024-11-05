using CarTroubleSolver.Shared.Models.ExtraModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTroubleSolver.Shared.ModelsConfiguration
{
    public class AccidentConfiguration : IEntityTypeConfiguration<Accident>
    {
        public void Configure(EntityTypeBuilder<Accident> builder)
        {
            builder.HasKey(m => m.Id);

            builder.HasMany(x => x.StatusHistory)
                 .WithOne(x => x.Accident)
                 .HasForeignKey(x => x.AccidentId);

            builder.HasOne(x => x.Car)
                .WithMany(x => x.Accidents)
                .HasForeignKey(x => x.CarId);

            builder.HasOne(x => x.Workshop)
                .WithMany(x => x.Accidents)
                .HasForeignKey(x => x.WorkshopId);
        }
    }
}