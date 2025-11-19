using CarTroubleSolver.Shared.Models.Enum;
using CarTroubleSolver.Shared.Models.UserPanel;
using CarTroubleSolver.Shared.Models.WorkshopPanel;

namespace CarTroubleSolver.Shared.Models.ExtraModels
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }

        public Guid? SenderUserId { get; set; }
        public virtual User SenderUser { get; set; }

        public Guid? SenderWorkshopId { get; set; }
        public virtual Workshop SenderWorkshop { get; set; }

        public Guid? ReceiverUserId { get; set; }
        public virtual User ReceiverUser { get; set; }

        public Guid? ReceiverWorkshopId { get; set; }
        public virtual Workshop ReceiverWorkshop { get; set; }
        public ServiceType Service{ get; set; }

        public Guid? CarId { get; set; }
        public virtual Car Car { get; set; }

        public Guid? PreviousMessageId { get; set; }
        public virtual Message PreviousMessage { get; set; }
        public DateTime? DateOfVisit { get; set; }

        public bool IsRead { get; set; }
        public bool Responsed { get; set; }
        public bool RateMessage { get; set; }
    }
}
