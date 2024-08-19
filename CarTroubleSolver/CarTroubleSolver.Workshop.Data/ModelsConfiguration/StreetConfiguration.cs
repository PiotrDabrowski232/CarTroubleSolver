using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CarTroubleSolver.Workshop.Data.Models;

namespace CarTroubleSolver.Workshop.Data.ModelsConfiguration
{
    public class StreetConfiguration : IEntityTypeConfiguration<Street>
    {
        public void Configure(EntityTypeBuilder<Street> builder)
        {
            builder.HasKey(w => w.Id);
        }
    }
}
