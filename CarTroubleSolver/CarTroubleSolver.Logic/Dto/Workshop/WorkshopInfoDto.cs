namespace CarTroubleSolver.Logic.Dto.Workshop
{
    public class WorkshopInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Services { get; set; }
        public double Rating { get; set; }
        public string City { get; set; }
        public double? Distance { get; set; }
    }
}
