using CarTroubleSolver.Shared.Extensions;
using CarTroubleSolver.Shared.Models.Enums;
using CarTroubleSolver.Shared.Models.UserPanel;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CarTroubleSolver.Shared.Models.ExtraModels;

namespace CarTroubleSolver.Shared.ModelsConfiguration
{
    public class RepairHistoryConfiguration : IEntityTypeConfiguration<RepairHistory>
    {
        public void Configure(EntityTypeBuilder<RepairHistory> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasMany(x => x.HistoryItems)
                .WithOne(x => x.RepairHistory)
                .HasForeignKey(x => x.RepairHistoryId);

            builder.HasOne(x => x.Car)
                .WithMany(x => x.RepairHistory)
                .HasForeignKey(x => x.CarId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Workshop)
                .WithMany(x => x.RepairHistory)
                .HasForeignKey(x => x.WorkshopId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
