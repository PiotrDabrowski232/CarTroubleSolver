using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Exceptions
{
    public class ControllerException : Exception
    {
        public ControllerException() { }
        public ControllerException(string message) : base(message) { }
    }
}
