using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Dto.Car
{
    public class BasicCarConfig
    {
        public IList<string> Brands { get; set; }
        public IList<string> Types { get; set; }
    }
}
