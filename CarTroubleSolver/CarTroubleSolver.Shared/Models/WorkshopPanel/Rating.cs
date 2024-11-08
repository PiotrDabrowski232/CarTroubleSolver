using CarTroubleSolver.Shared.Models.UserPanel;

namespace CarTroubleSolver.Shared.Models.WorkshopPanel
{
    public class Rating
    {
        public Guid Id { get; set; }
        public string Comment { get; set; }
        public int Rate { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid WorkshopId { get; set; }
        public virtual Workshop Workshop { get; set; }
    }
}
