using CarTroubleSolver.Shared.Models;

namespace CarTroubleSolver.Data.Models
{
    public class User : Account
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
