using CarTroubleSolver.Shared.Data;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using CarTroubleSolver.Shared.Repositories.Interfaces;

namespace CarTroubleSolver.Shared.Repositories
{
    public class WorkshopRepository(CarTroubleSolverDbContext dbContext) : GenericRepository<Workshop>(dbContext), IGenericRepository<Workshop>, IWorkshopRepository
    {
        public bool WorkshopExist(long nip)
        {
            return dbContext.Workshops.FirstOrDefault(x => x.NIP == nip) != null;
        }

        public Guid Add(Workshop workshop)
        {
            workshop.Id = Guid.NewGuid();
            base.Add(workshop);
            return workshop.Id;
        }
    }
}
