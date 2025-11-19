namespace CarTroubleSolver.Workshop.Logic.Dto.Message
{
    public class ReceiveMessageDto
    {
        public string Description { get; set; }
        public string UserName { get; set; }
        public int Telephone { get; set; }
        public string Service {  get; set; }
        public Guid WorkshopId { get; set; }
        public Guid UserId { get; set; }
        public CarDto Car { get; set; }
        public DateTime SendDay { get; set; }
        public bool IsRead { get; set; }

    }
}
