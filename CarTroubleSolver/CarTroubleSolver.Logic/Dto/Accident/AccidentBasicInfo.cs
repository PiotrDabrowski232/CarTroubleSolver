using CarTroubleSolver.Shared.Models.Enum;

namespace CarTroubleSolver.Logic.Dto.Accident
{
    public class AccidentBasicInfo
    {
        public Guid Id { get; set; }
        public string WorkshopName { get; set; }
        public string Service { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
    }
}
