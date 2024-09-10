using CarTroubleSolver.Shared.Models;

namespace CarTroubleSolver.Workshop.Data.Models
{
    public class Workshop : Account
    {
        public long NIP {  get; set; }
        public string Name { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }

    }
}
