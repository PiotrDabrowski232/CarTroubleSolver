using CarTroubleSolver.Shared.Models.Enum;

namespace CarTroubleSolver.Logic.Dto.Message
{
    public class ReceivedMessageDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public string WorkshopName { get; set; }
        public bool IsRead { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime? DateOfVisit { get; set; }

        public string Brand { get; set; }
        public string Model { get; set; }
        public string Service { get; set; }
    }
}
