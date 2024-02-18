using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Dto.User
{
    public class ChangePasswordUserDto
    {
        public Guid Id { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmedNewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
