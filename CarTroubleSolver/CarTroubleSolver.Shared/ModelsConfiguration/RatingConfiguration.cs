using CarTroubleSolver.Shared.Models.WorkshopPanel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTroubleSolver.Shared.ModelsConfiguration
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Workshop)
                .WithMany(x => x.Ratings)
                .HasForeignKey(x => x.WorkshopId);
        }
    }
}
