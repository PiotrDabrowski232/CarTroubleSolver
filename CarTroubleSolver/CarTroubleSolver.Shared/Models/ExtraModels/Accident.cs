using CarTroubleSolver.Shared.Models.Enum;
using CarTroubleSolver.Shared.Models.UserPanel;
using CarTroubleSolver.Shared.Models.WorkshopPanel;

namespace CarTroubleSolver.Shared.Models.ExtraModels
{
    public class Accident
    {
        public Guid Id { get; set; }
        public Guid WorkshopId {  get; set; }
        public virtual Workshop Workshop { get; set; }
        public Guid CarId { get; set; }
        public virtual Car Car { get; set; }

        public ServiceType Service {  get; set; }
        public ICollection<StatusHistory> StatusHistory { get; set; }
        public DateTime StartDate { get; set; }
    }
}
