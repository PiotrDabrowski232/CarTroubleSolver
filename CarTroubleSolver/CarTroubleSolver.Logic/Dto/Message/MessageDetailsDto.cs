using CarTroubleSolver.Logic.Dto.Car;

namespace CarTroubleSolver.Logic.Dto.Message
{
    public class MessageDetailsDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string WorkshopName { get; set; }
        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime? DateOfVisit { get; set; }

        public CarBasicInfoMessageDto Car { get; set; }
        public string Service {  get; set; }
    }
}
