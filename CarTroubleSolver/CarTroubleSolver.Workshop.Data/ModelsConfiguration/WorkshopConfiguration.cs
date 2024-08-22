﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTroubleSolver.Workshop.Data.ModelsConfiguration
{
    internal class WorkshopConfiguration : IEntityTypeConfiguration<Models.Workshop>
    {
        public void Configure(EntityTypeBuilder<Models.Workshop> builder)
        {
            builder.HasKey(w => w.Id);
        }
    }
}