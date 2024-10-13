using CarTroubleSolver.Shared.Models.Enum;

namespace CarTroubleSolver.Shared.Models.WorkshopPanel
{
    public class WorkshopServices
    {
        public Guid Id { get; set; }
        public ServiceType Service {  get; set; }
        public decimal Price { get; set; }

        public virtual Workshop Workshop { get; set; }
        public Guid WorkshopId { get; set; }
    }
}
