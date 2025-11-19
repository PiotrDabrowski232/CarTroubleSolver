namespace CarTroubleSolver.Workshop.Logic.Dto
{
    public class RepairHistoryDto
    {
        public string Service { get; set; }
        public int Price { get; set; }
        public float SpentHours { get; set; }
        public List<HistoryItemDto>? HistoryItems { get; set; }
    }
}
