using CarTroubleSolver.Shared.Models.WorkshopPanel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTroubleSolver.Shared.ModelsConfiguration
{
    internal class HourConfigurationModelConfiguration : IEntityTypeConfiguration<HourConfiguration>
    {
        public void Configure(EntityTypeBuilder<HourConfiguration> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.From)
                .HasConversion(
                    v => v.ToTimeSpan(),
                    v => TimeOnly.FromTimeSpan(v));

            builder.Property(w => w.To)
                .HasConversion(
                    v => v.ToTimeSpan(),
                    v => TimeOnly.FromTimeSpan(v));
        }
    }
}
