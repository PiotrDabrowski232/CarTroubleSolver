using CarTroubleSolver.Shared.Models.WorkshopPanel;

namespace CarTroubleSolver.Shared.Repositories.Interfaces
{
    public interface IWorkshopRepository : IGenericRepository<Workshop>
    {
        public bool WorkshopExist(long nip);
        public Guid Add(Workshop workshop);
    }
}
