using CarTroubleSolver.Shared.Models.UserPanel;
using CarTroubleSolver.Shared.Models.WorkshopPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
