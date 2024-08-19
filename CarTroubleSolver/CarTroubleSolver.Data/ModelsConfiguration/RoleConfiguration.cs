﻿using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Data.Models.Enums;
using CarTroubleSolver.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTroubleSolver.Data.ModelsConfiguration
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(u => u.Id);

            var roles = new Role[]
                {
                    CreateRole(1.ToGuid(), RoleName.BasicUser.ToString()),
                    CreateRole(2.ToGuid(), RoleName.Mechanic.ToString()),
                    CreateRole(3.ToGuid(), RoleName.MechanicalWorkshop.ToString()),
                    CreateRole(4.ToGuid(), RoleName.Admin.ToString()),
                };

            builder.HasData(roles);
        }

        private static Role CreateRole(Guid id, string name)
        {
            return new Role
            {
                Id = id,
                Name = name
            };
        }
    }

}
