namespace CarTroubleSolver.Shared.Models
{
    public class StreetDto
    {
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }

        public string StreetToString()
        {
            return $"{StreetName} {StreetNumber}, {PostalCode} {City}, {Province}, {Country}";
        }
    }
}
