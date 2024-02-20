using CarTroubleSolver.Data.Extensions;
using CarTroubleSolver.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

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
