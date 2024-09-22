using CarTroubleSolver.Shared.Models.UserPanel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTroubleSolver.Data.ModelsConfiguration
{
    internal class ColorConfiguration : IEntityTypeConfiguration<CarColor>
    {
        public void Configure(EntityTypeBuilder<CarColor> builder)
        {
            builder.HasKey(u => u.Id);

        }
    }
}
