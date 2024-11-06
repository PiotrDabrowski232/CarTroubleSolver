using CarTroubleSolver.Shared.Models.Enum;
using CarTroubleSolver.Shared.Models.UserPanel;
using CarTroubleSolver.Shared.Models.WorkshopPanel;

namespace CarTroubleSolver.Shared.Models.ExtraModels
{
    public class RepairHistory
    {
        public Guid Id { get; set; }
        public ServiceType Service { get; set; }
        public int Price { get; set; }
        public float SpentHours { get; set; }
        public Guid CarId { get; set; }
        public virtual Car Car { get; set; }
        public Guid WorkshopId { get; set; }
        public virtual Workshop Workshop { get; set; }
        public Guid AccidentId { get; set; }
        public virtual Accident Accident { get; set; }
        public virtual ICollection<HistoryItems> HistoryItems { get; set; }

    }
}
