using CarTroubleSolver.Shared.Models;

namespace CarTroubleSolver.Workshop.Data.Models
{
    public class Workshop : Account
    {
        public long NIP {  get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
