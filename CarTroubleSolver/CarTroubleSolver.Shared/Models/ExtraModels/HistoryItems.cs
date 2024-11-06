namespace CarTroubleSolver.Shared.Models.ExtraModels
{
    public class HistoryItems
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Amount { get; set; }

        public Guid RepairHistoryId { get; set; }
        public virtual RepairHistory RepairHistory { get; set; }
    }
}
