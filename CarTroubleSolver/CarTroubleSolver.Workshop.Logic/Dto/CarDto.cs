using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Workshop.Logic.Dto
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public long Mileage { get; set; }
        public string Engine { get; set; }
    }
}
