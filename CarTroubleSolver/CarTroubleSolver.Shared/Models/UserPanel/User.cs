using CarTroubleSolver.Shared.Models.ExtraModels;

namespace CarTroubleSolver.Shared.Models.UserPanel
{
    public class User : Account
    {
        public string Surname { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
