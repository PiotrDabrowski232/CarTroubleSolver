using CarTroubleSolver.Workshop.Data.Repositories.Interfaces;
using CarTroubleSolver.Workshop.Logic.Dto.Workshop;

namespace CarTroubleSolver.Workshop.Data.Repositories
{
    public class WorkshopRepository(CarTroubleSolverDbContext dbContext) : GenericRepository<Models.Workshop>(dbContext), IGenericRepository<Models.Workshop>, IWorkshopRepository
    {
        public bool WorkshopExist(RegisterWorkshopDto workshop)
        {
            return dbContext.Workshops.FirstOrDefault(x => x.NIP == workshop.NIP) != null;
        }
    }
}
