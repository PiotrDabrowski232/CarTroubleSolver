using CarTroubleSolver.Shared.Models.UserPanel;

namespace CarTroubleSolver.Logic.Services.Interfaces
{
    public interface IRoleService
    {
        public Role GetRole(Guid roleId);
    }
}
