namespace CarTroubleSolver.Workshop.Logic.Dto.Message
{
    public class ReceiveMessageBasicInfoDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public int Telephone { get; set; }
        public string Service {  get; set; }
        public BasicCarInfoDto Car { get; set; }
        public DateTime SendDay { get; set; }
        public bool IsRead { get; set; }
    }
}
