using CarTroubleSolver.Workshop.Data.Repositories.Interfaces;

namespace CarTroubleSolver.Workshop.Data.Repositories
{
    public class WorkshopRepository(CarTroubleSolverDbContext dbContext) : GenericRepository<Models.Workshop>(dbContext), IGenericRepository<Models.Workshop>, IWorkshopRepository
    {
        public bool WorkshopExist(long nip)
        {
            return dbContext.Workshops.FirstOrDefault(x => x.NIP == nip) != null;
        }

        public Guid Add(Models.Workshop workshop)
        {
            workshop.Id = Guid.NewGuid();
            base.Add(workshop);
            return workshop.Id;
        }
    }
}
