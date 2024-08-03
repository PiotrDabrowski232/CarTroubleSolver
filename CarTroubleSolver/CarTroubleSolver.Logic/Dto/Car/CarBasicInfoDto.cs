using CarTroubleSolver.Data.Models.Enums;

namespace CarTroubleSolver.Logic.Dto.Car
{
    public class CarBasicInfoDto
    {
        public long VIN { get; set; }
        public Color Color { get; set; }
        public string CarType { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
    }
}
