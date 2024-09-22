using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Shared.Models.UserPanel
{
    public class CarColor
    {
        public Guid Id { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

        public CarColor()
        {
            Id = Guid.NewGuid();
        }
    }
}
