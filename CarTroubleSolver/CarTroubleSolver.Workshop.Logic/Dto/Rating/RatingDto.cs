namespace CarTroubleSolver.Workshop.Logic.Dto.Rating
{
    public class RatingDto
    {
        public double Average { get; set; }
        public string WorkshopName { get; set; }
        public List<RatingItemsDto> RatingItems { get; set; }
    }
}
