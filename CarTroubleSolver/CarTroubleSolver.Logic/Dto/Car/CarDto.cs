namespace CarTroubleSolver.Logic.Dto.Car
{
    public class CarDto
    {
        public long VIN { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public Color Color { get; set; }
        public int DoorCount { get; set; }
        public DateTime DateOfProduction { get; set; }
        public string Type { get; set; }
    }
}
