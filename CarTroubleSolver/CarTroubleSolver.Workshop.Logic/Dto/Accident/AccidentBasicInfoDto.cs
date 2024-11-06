namespace CarTroubleSolver.Workshop.Logic.Dto.Accident
{
    public class AccidentBasicInfoDto
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public long Vin { get; set; }
        public string Engine { get; set; }
        public string Service { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}
