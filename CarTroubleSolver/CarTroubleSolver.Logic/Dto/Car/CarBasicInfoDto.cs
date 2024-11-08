namespace CarTroubleSolver.Logic.Dto.Car
{
    public class CarBasicInfoDto
    {
        public Guid Id { get; set; }
        public string VIN { get; set; }
        public Color Color { get; set; }
        public string CarType { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int DoorCount { get; set; }
        public DateTime DateOfProduction { get; set; }
    }
}
