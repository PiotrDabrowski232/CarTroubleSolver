using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Dto.Car
{
    public class CarBasicInfoDto
    {
        public long VIN { get; set; }
        public Color Color { get; set; }
        public string CarType { get; set; }
    }
}
