namespace CarTroubleSolver.Logic.Dto
{
    public class MessageDto
    {
        public Guid WorkshopId { get; set; }
        public string Service { get; set; }
        public Guid CarId { get; set; }
        public string Description { get; set; }
    }
}
