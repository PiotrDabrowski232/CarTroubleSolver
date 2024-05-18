using CarTroubleSolver.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Dto.Car
{
    public class CarDto
    {
        public long VIN { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int DoorCount { get; set; }
        public DateOnly DateOfProduction { get; set; }
        public string CarType { get; set; }
    }
}
