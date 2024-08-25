using CarTroubleSolver.Workshop.Logic.Dto.Workshop;

namespace CarTroubleSolver.Workshop.Data.Repositories.Interfaces
{
    public interface IWorkshopRepository : IGenericRepository<Models.Workshop>
    {
        public bool WorkshopExist(RegisterWorkshopDto workshop);
    }
}
