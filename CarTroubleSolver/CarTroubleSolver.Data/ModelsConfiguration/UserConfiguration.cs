using CarTroubleSolver.Shared.Extensions;
using CarTroubleSolver.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTroubleSolver.Data.ModelsConfiguration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder
        .Property(u => u.DateOfBirth)
        .HasColumnType("date");

            builder.Property(u => u.RoleId)
                .HasDefaultValue(1.ToGuid());
        }
    }
}
