using CarTroubleSolver.Data.Models.Enums;

namespace CarTroubleSolver.Data.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public long VIN { get; set; }
        public Brand Brand { get; set; }
        public string Model { get; set; }
        public long Mileage { get; set; }
        public string Engine { get; set; }
        public int DoorCount { get; set; }
        public DateTime DateOfProduction { get; set; }
        public CarType CarType { get; set; }
        public string? ImagePath { get; set; }

        public Guid OwnerId { get; set; }
        public virtual User Owner { get; set; }

        public Guid ColorId { get; set; }
        public virtual CarColor Color { get; set; }

    }
}