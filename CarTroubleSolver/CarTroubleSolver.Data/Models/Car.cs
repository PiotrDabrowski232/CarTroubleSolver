using CarTroubleSolver.Data.Models.Enums;

namespace CarTroubleSolver.Data.Models
{
    public class Car
    {
        public long VIN { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string Color { get; set; }
        public int DoorCount { get; set; }
        public DateTime DateOfProduction { get; set; }
        public CarType CarType { get; set; }

        public Guid OwnerId { get; set; }
        public virtual User Owner { get; set; }
    }
}