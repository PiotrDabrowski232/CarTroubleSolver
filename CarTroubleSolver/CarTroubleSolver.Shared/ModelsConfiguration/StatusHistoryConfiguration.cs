using CarTroubleSolver.Shared.Models.ExtraModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTroubleSolver.Shared.ModelsConfiguration
{
    internal class StatusHistoryConfiguration : IEntityTypeConfiguration<StatusHistory>
    {
        public void Configure(EntityTypeBuilder<StatusHistory> builder)
        {
            builder.HasKey(m => m.Id);
        }
    }
}