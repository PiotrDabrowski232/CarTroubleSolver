
namespace CarTroubleSolver.Workshop.Data.Repositories.Interfaces
{
    public interface IWorkshopRepository : IGenericRepository<Models.Workshop>
    {
        public bool WorkshopExist(long nip);
        public Guid Add(Models.Workshop workshop);
    }
}
