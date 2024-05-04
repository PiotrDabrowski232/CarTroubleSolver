using CarTroubleSolver.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarTroubleSolver.Data.Extensions;
using CarTroubleSolver.Data.Models.Enums;

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
