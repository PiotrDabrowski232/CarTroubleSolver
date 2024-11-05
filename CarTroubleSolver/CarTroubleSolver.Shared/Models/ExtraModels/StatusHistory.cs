using CarTroubleSolver.Shared.Models.Enum;

namespace CarTroubleSolver.Shared.Models.ExtraModels
{
    public class StatusHistory
    {
        public Guid Id { get; set; }
        public AccidentStatus Status { get; set; }
        public DateTime Date { get; set; }

        public Guid AccidentId { get; set; }
        public virtual Accident Accident { get; set; }
    }
}
