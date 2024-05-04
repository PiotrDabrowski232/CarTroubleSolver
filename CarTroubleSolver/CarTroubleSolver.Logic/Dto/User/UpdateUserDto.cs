using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Dto.User
{
    public class UpdateUserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}
