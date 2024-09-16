using CarTroubleSolver.Shared.Models;

namespace CarTroubleSolver.Workshop.Data.Models
{
    public class Workshop : Account
    {
        public long NIP {  get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
