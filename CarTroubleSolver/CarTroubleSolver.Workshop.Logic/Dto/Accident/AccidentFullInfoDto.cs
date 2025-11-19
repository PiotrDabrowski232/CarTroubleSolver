namespace CarTroubleSolver.Workshop.Logic.Dto.Accident
{
    public class AccidentFullInfoDto
    {
        public Guid Id { get; set; }
        public CarDto Car { get; set; }
        public string Service { get; set; }
        public string Status { get; set; }
        public string UserMessage { get; set; }
        public string UserName { get; set; }
        public string UserContact { get; set; }
        public DateTime Date { get; set; }
    }
}
