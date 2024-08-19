using CarTroubleSolver.Shared.Models;

namespace CarTroubleSolver.Workshop.Data.Models
{
    public class Workshop : Account
    {
        public long NIP {  get; set; }


        public Guid StreetId { get; set; }
        public virtual Street Street { get; set; }
    }
}
