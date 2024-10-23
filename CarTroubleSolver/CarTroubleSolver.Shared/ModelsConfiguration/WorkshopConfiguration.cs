using CarTroubleSolver.Shared.Models.WorkshopPanel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTroubleSolver.Shared.ModelsConfiguration
{
    internal class WorkshopConfiguration : IEntityTypeConfiguration<Workshop>
    {
        public void Configure(EntityTypeBuilder<Workshop> builder)
        {
            builder.HasKey(w => w.Id);

            builder.HasMany(p => p.OpenHours)
                .WithOne(c => c.Workshop)
                .HasForeignKey(c => c.WorkshopId);

            builder.HasMany(p => p.Services)
                .WithOne(c => c.Workshop)
                .HasForeignKey(c => c.WorkshopId);

            builder.Property(w => w.Location)
                .HasColumnType("geography");
        }
    }
}
