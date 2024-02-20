using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Data.Repositories.Interfaces;
using CarTroubleSolver.Logic.Services.Interfaces;

namespace CarTroubleSolver.Logic.Services
{
    public class RoleService(IRoleRepository roleRepository) : IRoleService
    {
        private readonly IRoleRepository _roleRepository = roleRepository;

        public Role GetRole(Guid roleId)
        {
            var result =_roleRepository.Get(roleId);
            return result.Result;
        }
    }
}
