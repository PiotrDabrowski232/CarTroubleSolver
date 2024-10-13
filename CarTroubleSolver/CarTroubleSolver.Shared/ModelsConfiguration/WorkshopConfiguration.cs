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

            builder.Property(w => w.Longitude)
                .HasColumnType("decimal(24,21)");

            builder.Property(w => w.Latitude)
                .HasColumnType("decimal(24,21)");

            builder.HasMany(p => p.OpenHours)
                .WithOne(c => c.Workshop)
                .HasForeignKey(c => c.WorkshopId);

            builder.HasMany(p => p.Services)
                .WithOne(c => c.Workshop)
                .HasForeignKey(c => c.WorkshopId);
        }
    }
}
