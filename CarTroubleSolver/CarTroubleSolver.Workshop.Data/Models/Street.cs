using CarTroubleSolver.Workshop.Data.Models.Enum;

namespace CarTroubleSolver.Workshop.Data.Models
{
    public class Street
    {
        public Guid Id { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Province Province { get; set; }
    }
}
