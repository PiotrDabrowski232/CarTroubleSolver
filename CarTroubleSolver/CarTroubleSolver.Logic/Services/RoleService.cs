using CarTroubleSolver.Logic.Services.Interfaces;
using CarTroubleSolver.Shared.Models.UserPanel;
using CarTroubleSolver.Shared.Repositories.Interfaces;

namespace CarTroubleSolver.Logic.Services
{
    public class RoleService(IRoleRepository roleRepository) : IRoleService
    {
        private readonly IRoleRepository _roleRepository = roleRepository;

        public Role GetRole(Guid roleId)
        {
            var result = _roleRepository.Get(roleId);
            return result.Result;
        }
    }
}
