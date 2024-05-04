using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
    }
}
