using CarTroubleSolver.Shared.Models.WorkshopPanel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTroubleSolver.Shared.ModelsConfiguration
{
    public class WorkshopServicesConfiguration : IEntityTypeConfiguration<WorkshopServices>
    {
        public void Configure(EntityTypeBuilder<WorkshopServices> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(x => x.Price)
                .HasPrecision(10, 2);
        }
    }
}
