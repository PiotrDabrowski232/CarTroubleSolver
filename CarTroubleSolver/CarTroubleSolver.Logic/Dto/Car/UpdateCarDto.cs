namespace CarTroubleSolver.Logic.Dto.Car
{
    public class UpdateCarDto
    {
        public long VIN { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int DoorCount { get; set; }
        public DateTime DateOfProduction { get; set; }
        public string Type { get; set; }
        public long Mileage { get; set; }
        public string Engine { get; set; }
        public long OldVin { get; set; }
    }
}
