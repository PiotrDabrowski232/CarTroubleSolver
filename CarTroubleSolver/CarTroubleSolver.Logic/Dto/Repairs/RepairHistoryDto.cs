namespace CarTroubleSolver.Logic.Dto.Repairs
{
    public class RepairHistoryDto
    {
        public Guid WorkshopId { get; set; }
        public string WorkshopName { get; set; }
        public string WorkshopNumber { get; set; }
        public string Service { get; set; }
        public int Price { get; set; }
        public float SpentHours { get; set; }
        public DateTime Date { get; set; }
        public List<HistoryItemDto>? HistoryItems { get; set; }
    }
}
