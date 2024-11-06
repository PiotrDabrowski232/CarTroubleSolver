using CarTroubleSolver.Shared.Models.ExtraModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTroubleSolver.Shared.ModelsConfiguration
{
    internal class HistoryItemsConfiguration : IEntityTypeConfiguration<HistoryItems>
    {
        public void Configure(EntityTypeBuilder<HistoryItems> builder)
        {
            builder.HasKey(u => u.Id);

        }
    }
}
