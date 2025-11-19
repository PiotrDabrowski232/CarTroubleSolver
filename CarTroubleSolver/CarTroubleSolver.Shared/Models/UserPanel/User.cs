using CarTroubleSolver.Shared.Models.ExtraModels;
using CarTroubleSolver.Shared.Models.WorkshopPanel;

namespace CarTroubleSolver.Shared.Models.UserPanel
{
    public class User : Account
    {
        public string Surname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; } 
        public virtual ICollection<Message> ReceivedMessages { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
