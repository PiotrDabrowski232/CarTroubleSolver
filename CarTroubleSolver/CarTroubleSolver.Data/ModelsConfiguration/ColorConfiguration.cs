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
    internal class ColorConfiguration : IEntityTypeConfiguration<CarColor>
    {
        public void Configure(EntityTypeBuilder<CarColor> builder)
        {
            builder.HasKey(u => u.Id);

        }
    }
}
