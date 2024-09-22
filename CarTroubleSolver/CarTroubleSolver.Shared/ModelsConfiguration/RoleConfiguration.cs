using CarTroubleSolver.Shared.Extensions;
using CarTroubleSolver.Shared.Models.Enums;
using CarTroubleSolver.Shared.Models.UserPanel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTroubleSolver.Shared.ModelsConfiguration
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
